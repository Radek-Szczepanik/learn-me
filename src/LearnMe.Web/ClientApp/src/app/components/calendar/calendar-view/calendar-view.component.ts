import { Component, OnInit } from '@angular/core';
import { CalendarService } from '../../../services/calendar/calendar-service'
import * as Calendarevent from "../../../services/calendar/calendar-event";
import CalendarEvent = Calendarevent.CalendarEvent;
import notify from 'devextreme/ui/notify';
import { HttpService } from "../../../services/http.service";
import CalendarEventPost = Calendarevent.CalendarEventPost;
import Scheduler from "devextreme/ui/scheduler";
import { Lesson, LessonStatus, EventLesson, AttendeeDto, UserBasicDto } from '../../../Models/Lesson/lesson'
import { User } from "../../../Models/Users/user"

//if (!/localhost/.test(document.location.host)) {
//  enableProdMode();
//}

@Component({
  selector: 'app-calendar-view',
  templateUrl: './calendar-view.component.html',
  styleUrls: ['./calendar-view.component.css']
})
export class CalendarViewComponent implements OnInit {

  appointmentsData: CalendarEvent[];
  lessons: Lesson[];
  currentDate: Date = new Date();
  timezone: string = "Europe/Warsaw";

  eventToAdd: CalendarEventPost;
  lessonToAdd: Lesson;

  appointmentFormUpdatedFlag: boolean = false;
  isFirstLoadFlag: boolean = true;
  isFirstLoadAfterEditingEvent: boolean = true;
  isDataLoaded: boolean = false;
  dataLoaded: boolean = false;
  dataReload: number = 0;

  startViewDate: Date;
  endViewDate: Date;

  currentLesson: Lesson;
  //currentAttendees: AttendeesEmails;
  lessonEmails: string[] = [];

  itemsLessonStatus: string[] = ["New", "InProgress", "Done"];

  simpleEmails = [];

  constructor(private data: CalendarService, private https: HttpService) {
    console.debug('appointmentsData:');
    console.debug(this.appointmentsData);

    this.eventToAdd = {
      subject: '',
      description: '',
      startDate: new Date(),
      endDate: new Date(),
      isDone: false,
      isFreeSlot: false,
      calendarId: ''
    };

    this.lessonToAdd = {
      title: '',
      lessonStatus: 0,
      relatedInvoiceId: null,
      calendarEventId: 0
    };

    this.currentLesson = {
      title: '',
      calendarEventId: 0,
      lessonStatus: 0,
      relatedInvoiceId: 0
    }

    let routeGetAllStudents = '/api/UserBasics?rolename=Student';
    this.https.getData(routeGetAllStudents)
      .toPromise().then(success => {
        if (success) {
          let students = success as User[];
          students.forEach(item => {
            this.simpleEmails.push(item.email);
          });
        }
      });
  }

  ngOnInit(): void { }

  onInitialized(e) { }

  onContentReady(e) {
    this.getCalendarCurrentDate();

    if (this.isFirstLoadFlag && this.isFirstLoadAfterEditingEvent) {
      console.debug('onContentReady isFirstLoadFlag');

      this.data.loadEventsByDates(this.startViewDate, this.endViewDate)
        .toPromise().then(success => {
          console.debug(success);
          if (success) {
            this.appointmentsData = this.data.events;
          }
        });

      this.isFirstLoadFlag = false;
      this.isFirstLoadAfterEditingEvent = false;

    } else {
      console.debug('onContentReady not 1st load');

      this.isFirstLoadFlag = true;
      this.isFirstLoadAfterEditingEvent = true;
    }
  }

  onAppointmentAdding(e) {
    console.error('onAppointmentAdding fired');
    this.isFirstLoadAfterEditingEvent = false;
  }

  onAppointmentAdded(e) {
    console.error("on appointment added invoked");
    console.debug(e.appointmentData.text);
    console.debug(e.appointmentData.subject);
    this.showToast("Added", e.appointmentData.text, "success");

    console.debug('object to be added:');
    console.debug(e);

    this.eventToAdd.subject = e.appointmentData.subject;
    this.eventToAdd.description = e.appointmentData.description;
    this.eventToAdd.startDate = e.appointmentData.startDate;
    this.eventToAdd.endDate = e.appointmentData.endDate;
    this.eventToAdd.isDone = e.appointmentData.isDone;
    this.eventToAdd.isFreeSlot = e.appointmentData.isFreeSlot;

    console.debug(this.eventToAdd);

    let externalCalendarId = '';

    this.https.post('/api/calendarevents', this.eventToAdd)
      .toPromise().then(success => {
        if (success) {
          console.debug('event added to DB and Calendar');
          console.debug(success);
          let eventAdded = success as CalendarEvent;
          externalCalendarId = eventAdded.calendarId;

          this.lessonToAdd.title = e.appointmentData.title;
          let lessonStatusIndex = this.itemsLessonStatus.findIndex(x => x == e.appointmentData.lessonStatus);
          this.lessonToAdd.lessonStatus = lessonStatusIndex;
          this.lessonToAdd.relatedInvoiceId = null;
          this.lessonToAdd.calendarEventId = 0;

          console.debug('lesson to add:');
          console.debug(this.lessonToAdd);

          let route = '/api/lessons/' + externalCalendarId;

          this.https.post(route, this.lessonToAdd)
            .toPromise().then(success => {
              if (success) {
                console.debug('lesson added to DB');

                let routeAttendees = '/api/lessons/' + externalCalendarId + '/attendees';

                console.error('attendeesEmails');
                console.debug(e.appointmentData.attendeesEmails);

                let emails: string[] = e.appointmentData.attendeesEmails;

                emails.forEach((email) => {
                  console.debug(email);
                  let attendeeEmailDto: AttendeeDto = {
                    attendeeEmail: email,
                  }
                  this.https.post(routeAttendees, attendeeEmailDto)
                    .toPromise().then(success => {
                      if (success) {
                        console.debug('user/attendee of lesson added to DB');
                      }
                    });
                });
              }
            });
        }
      });
  }

  onAppointmentUpdating(e) {
    this.isFirstLoadAfterEditingEvent = false;
  }

  onAppointmentUpdated(e) {
    console.error('onAppointmentUpdated fired');

    this.showToast("Updated", e.appointmentData.subject, "info");

    console.debug('when updated object is:');
    console.debug(e);

    this.eventToAdd.subject = e.appointmentData.subject;
    this.eventToAdd.description = e.appointmentData.description;
    this.eventToAdd.startDate = e.appointmentData.startDate;
    this.eventToAdd.endDate = e.appointmentData.endDate;
    this.eventToAdd.isDone = e.appointmentData.isDone;
    this.eventToAdd.isFreeSlot = e.appointmentData.isFreeSlot;
    this.eventToAdd.calendarId = e.appointmentData.calendarId;

    console.debug(this.eventToAdd);

    this.https.put('/api/calendareventsbygoogleid/', this.eventToAdd)
      .toPromise().then(success => {
        if (success) {
          console.debug('event updated in DB and Calendar');
        }
      });

    let putLessonUrl = '/api/lessons/' + e.appointmentData.calendarId;

    this.lessonToAdd.title = e.appointmentData.title;
    console.error('e.appointmentData.lessonStatus');
    console.error(e.appointmentData.lessonStatus);
    let lessonStatusIndex = this.itemsLessonStatus.findIndex(x => x == e.appointmentData.lessonStatus);
    this.lessonToAdd.lessonStatus = lessonStatusIndex;
    this.lessonToAdd.relatedInvoiceId = null;
    this.lessonToAdd.calendarEventId = 0;

    console.debug('lesson to add');
    console.debug(this.lessonToAdd);

    this.https.put(putLessonUrl, this.lessonToAdd)
      .toPromise().then(success => {
        if (success) {
          console.debug('lesson updated in DB and Calendar');
        }
      });

    // Attendees update
    // Emails from UI
    let emailsUI: string[] = e.appointmentData.attendeesEmails;
    // Emails from DB
    let emailsDB: string [] = [];
    let externalCalendarId = this.eventToAdd.calendarId;
    let routeAttendees = '/api/lessons/' + externalCalendarId + '/attendees';
    this.https.getData(routeAttendees)
      .toPromise().then(success => {
        if (success) {
          let dbEmails = success as AttendeeDto[];
          dbEmails.forEach(item => {
            emailsDB.push(item.attendeeEmail);
          });
          console.debug('user/attendee of lesson added to DB');
        }
      });

    let differenceToAdd = emailsUI.filter(x => !emailsDB.includes(x));
    let differenceToDelete = emailsDB.filter(x => !emailsUI.includes(x));

    differenceToAdd.forEach((email) => {
      console.debug('email added');
      console.debug(email);
      let attendeeEmailDto: AttendeeDto = {
        attendeeEmail: email,
      }
      this.https.post(routeAttendees, attendeeEmailDto)
        .toPromise().then(success => {
          if (success) {
            console.debug('user/attendee of lesson added to DB');
          }
        });
    });

    differenceToDelete.forEach((email) => {
      console.debug('email deleted');
      console.debug(email);
      //let attendeeEmailDto: AttendeeDto = {
      //  attendeeEmail: email,
      //}
      let routeAttendeeDelete = routeAttendees + '/' + email;
      this.https.delete(routeAttendeeDelete)
        .toPromise().then(success => {
          if (success) {
            console.debug('user/attendee of lesson deleted from DB');
          }
        });
    });
  }

  onAppointmentDeleting(e) {
    this.isFirstLoadAfterEditingEvent = false;
  }

  onAppointmentDeleted(e) {
    this.showToast("Deleted", e.appointmentData.subject, "warning");

    console.debug('when deleted object is:');
    console.debug(e);

    let deleteLessonUrl = '/api/lessons/' + e.appointmentData.calendarId;

    this.https.delete(deleteLessonUrl)
      .toPromise().then(success => {
        if (success) {
          console.debug('lesson deleted from DB and Calendar');
        }
      });

    let deleteUrl = '/api/calendareventsbygoogleid/' + e.appointmentData.calendarId;

    this.https.delete(deleteUrl)
      .toPromise().then(success => {
        if (success) {
          console.debug('event deleted from DB and Calendar');
        }
      });
  }

  onAppointmentClick(e) {
    console.debug('on appointment click fired');

    let externalCalendarId = e.appointmentData.calendarId;

    let route = '/api/lessons/' + externalCalendarId;

    this.https.getData(route)
      .toPromise().then(success => {
        if (success) {
          console.debug('lesson fetched from DB');
          console.debug(success);
          let lesson = success as Lesson;

          // ----
          this.currentLesson = lesson;
          console.debug('current lesson');
          console.debug(this.currentLesson);
          // ----

          e.appointmentData.title = lesson.title;
          e.appointmentData.lessonStatus = lesson.lessonStatus;
          console.debug('e.appointmentData');
          console.debug(e.appointmentData);
        } else {
          this.currentLesson.title = "";
          this.currentLesson.calendarEventId = -1;
        }
      });

    //Attendees
    let routeAttendees = '/api/lessons/' + externalCalendarId + '/attendees';
    //let lessonEmails: string[] = [];
    this.lessonEmails = [];

    this.https.getData(routeAttendees)
      .toPromise().then(success => {
        if (success) {
          console.error('success get attendees');
          console.debug(success);

          let emailObjects = success as UserBasicDto[];
          emailObjects.forEach(
            (item) => {
              console.error('get lesson attendees - item email');
              console.debug(item.email);
              this.lessonEmails.push(item.email);
              console.debug(this.lessonEmails);
            });
          console.debug('this.lessonEmails - final');
          console.debug(this.lessonEmails);
        }
      });
  }

  onAppointmentDoubleClick(e) {
    console.debug('on appointment double click fired');
    this.onAppointmentClick(e);
    this.onAppointmentFormOpening(e);
  }

  async onAppointmentFormOpening(e) {
    console.debug("onAppointmentFormOpening fired!");

    if (!this.appointmentFormUpdatedFlag) {
      let formItems = e.form.itemOption("mainGroup").items;

      console.debug('formItems');
      console.debug(formItems);

      formItems.push(
        {
          dataField: "isDone",
          editorType: "dxSwitch",
          label: {
            text: "Lesson Done"
          }
        },
        {
          dataField: "isFreeSlot",
          editorType: "dxSwitch",
          label: {
            text: "Free Slot"
          }
        },
        {
          itemType: "group",
          caption: "Lesson Data",
          items: [
            {
              dataField: "title",
              editorType: "dxTextArea",
              label: {
                text: "Lesson Title"
              },
              editorOptions: {
                value: "" //"Please add Lesson title"
              }
            },
            {
              dataField: "lessonStatus",
              editorType: "dxSelectBox",
              label: {
                text: "Lesson Status"
              },
              editorOptions: {
                items: this.itemsLessonStatus,
                value: this.itemsLessonStatus[0]
              },
            }],
          colSpan: 2
        },
        {
          itemType: "group",
          caption: "Attendees",
          items: [
            {
              dataField: "attendeesEmails",
              editorType: "dxTagBox",
              label: {
                text: "Attendees' emails:"
              },
              editorOptions: {
                items: this.simpleEmails,
                searchEnabled: true,
                hideSelectedItems: true
                //dataSource: new DevExpress.data.ArrayStore({
                //  data: products,
                //  key: "ID"
                //}),
                //displayExpr: "Name",
                //valueExpr: "ID",
              }
            }],
          colSpan: 2
        });

      e.form.itemOption("mainGroup",
        {
          items: formItems
        });

      this.appointmentFormUpdatedFlag = true;
    }

    // ----- CODE DUPLICATION START -----
    let externalCalendarId = e.appointmentData.calendarId;

    let route = '/api/lessons/' + externalCalendarId;

    this.https.getData(route)
      .toPromise().then(success => {
        if (success) {
          console.debug('lesson fetched from DB');
          console.debug(success);
          let lesson = success as Lesson;

          // ----
          this.currentLesson = lesson;
          console.debug('current lesson');
          console.debug(this.currentLesson);
          // ----

          e.appointmentData.title = lesson.title;
          e.appointmentData.lessonStatus = lesson.lessonStatus;
          console.debug('e.appointmentData');
          console.debug(e.appointmentData);
        } else {
          this.currentLesson.title = "";
          this.currentLesson.calendarEventId = -1;
        }

        e.form.itemOption("mainGroup").items[8].items[0].editorOptions.value = this.currentLesson.title;
        e.form.itemOption("mainGroup").items[8].items[1].editorOptions.value = this.itemsLessonStatus[this.currentLesson.lessonStatus];

        ////Attendees
        //let routeAttendees = '/api/lessons/' + externalCalendarId + '/attendees';
        //let lessonEmails: string[] = [];

        //this.https.getData(routeAttendees)
        //  .toPromise().then(success => {
        //    if (success) {
        //      console.error('success get attendees');
        //      console.debug(success);

        //      let emailObjects = success as UserBasicDto[];
        //      emailObjects.forEach(
        //        (item) => {
        //          lessonEmails.push(item.email);
        //        });

        //      console.debug(lessonEmails);
        //      e.form.itemOption("mainGroup").items[9].items[0].editorOptions.value = lessonEmails;
        //    }
        //  });

        //console.error('debug attendees');
        //console.debug(e.form.itemOption("mainGroup").items);

        //e.form.itemOption("mainGroup").items[9].items[0].editorOptions.value = [this.simpleEmails[0], this.simpleEmails[1]];

        ////OK
        let commonAttendees: string[] = this.simpleEmails.filter(value => this.lessonEmails.includes(value));
        console.debug('commonAttendees');
        console.debug(commonAttendees);

        let lessonStatusIndexes = [];
        commonAttendees.forEach(item => {
          let index = this.simpleEmails.findIndex(x => x);
          lessonStatusIndexes.push(index);
        });
        console.debug('lesson status Indexes');
        console.debug(lessonStatusIndexes);

        let attendeesValues = [];
        lessonStatusIndexes.forEach(i => {
          attendeesValues.push(this.simpleEmails[i]);
        });
        console.debug('attendeesValues');
        console.debug(attendeesValues);

        //e.form.itemOption("mainGroup").items[9].items[0].editorOptions.value = [commonAttendees[0], commonAttendees[1]];
        e.form.itemOption("mainGroup").items[9].items[0].editorOptions.value = commonAttendees;

        //// OK
        //e.form.itemOption("mainGroup").items[9].items[0].editorOptions.value = [this.simpleEmails[lessonStatusIndex]];
        //e.form.itemOption("mainGroup").items[9].items[0].editorOptions.value = [[this.simpleEmails[0]]];

        e.form.itemOption("mainGroup.subject",
          {
            validationRules: [
              {
                type: "required",
                message: "Subject is required"
              }
            ]
          });

        console.debug(e.form.itemOption("mainGroup").items);
        console.debug(this.appointmentFormUpdatedFlag);

      });
    // ----- CODE DUPLICATION END -----

    //e.form.itemOption("mainGroup").items[8].items[0].editorOptions.value = this.currentLesson.title;
    //e.form.itemOption("mainGroup").items[8].items[1].editorOptions.value = this.itemsLessonStatus[this.currentLesson.lessonStatus];


    //  e.form.itemOption("mainGroup.subject",
    //    {
    //      validationRules: [
    //        {
    //          type: "required",
    //          message: "Subject is required"
    //        }
    //      ]
    //    });

    //console.debug(e.form.itemOption("mainGroup").items);
    //console.debug(this.appointmentFormUpdatedFlag);
  }

  getCalendarCurrentDate() {
    let element = document.getElementById("myScheduler");
    let instance = Scheduler.getInstance(element) as Scheduler;

    this.startViewDate = instance.getStartViewDate();
    this.endViewDate = instance.getEndViewDate();
  }

  showToast(event, value, type) {
    notify(event + " \"" + value + "\"" + " task", type, 1800);
  }
}
