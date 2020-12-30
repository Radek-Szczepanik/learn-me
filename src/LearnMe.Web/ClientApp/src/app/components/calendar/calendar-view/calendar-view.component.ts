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

import {Appointment, Service} from '../../../services/calendar/calendar-service-ver-2';

// import * as AspNetData from "devextreme-aspnet-data-nojquery";

import DataSource from 'devextreme/data/data_source';
import CustomStore from 'devextreme/data/custom_store';

//if (!/localhost/.test(document.location.host)) {
//  enableProdMode();
//}

@Component({
  selector: 'app-calendar-view',
  templateUrl: './calendar-view.component.html',
  styleUrls: ['./calendar-view.component.css'],
  providers: [Service]
})
export class CalendarViewComponent implements OnInit {

  // appointmentsData: CalendarEvent[];
  // appointmentsData: Appointment[];
  appointmentsData: any;

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

  //startViewDate: Date;
  //endViewDate: Date;
  startViewDate: Date = new Date(new Date().getTime() - (31 * 24 * 60 * 60 * 1000));
  endViewDate: Date = new Date(new Date().getTime() + (31 * 24 * 60 * 60 * 1000));


  currentLesson: Lesson;
  //currentAttendees: AttendeesEmails;
  lessonEmails: string[] = [];

  itemsLessonStatus: string[] = ["New", "InProgress", "Done"];

  simpleEmails: string[] = [];

  constructor(private data: CalendarService, private https: HttpService, service: Service) {

    // var url = "/api/CalendarEventsByDate";

    this.appointmentsData = new DataSource({
      store: new CustomStore({
          load: (options) => this.data.loadEventsByDates(this.startViewDate, this.endViewDate)
            .toPromise().then(success => {
              console.debug(success);
              if (success) {
                this.appointmentsData = this.data.events;
              }
            }),
          // insert: (options) => this.data.createEvent(this.appointmentsData.current)
          // .toPromise().then(success => {
          //   console.debug(success);
          // }),
          // update:,
          // remove:,
      })
    });

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
    
    console.debug('simpleEmails:');
    console.debug(this.simpleEmails);

    // this.data.loadEventsByDates(this.startViewDate, this.endViewDate)
    //   .toPromise().then(success => {
    //     console.debug(success);
    //     if (success) {
    //       this.appointmentsData = this.data.events;
    //     }
    //   });

  }

  ngOnInit(): void { }

  onInitialized(e) { }

  //onContentReady(e) {
  //  this.getCalendarCurrentDate();

  //  if (this.isFirstLoadFlag && this.isFirstLoadAfterEditingEvent) {
  //    console.debug('onContentReady isFirstLoadFlag');

  //    this.data.loadEventsByDates(this.startViewDate, this.endViewDate)
  //      .toPromise().then(success => {
  //        console.debug(success);
  //        if (success) {
  //          this.appointmentsData = this.data.events;
  //        }
  //      });

  //    this.isFirstLoadFlag = false;
  //    this.isFirstLoadAfterEditingEvent = false;

  //  } else {
  //    console.debug('onContentReady not 1st load');

  //    this.isFirstLoadFlag = true;
  //    this.isFirstLoadAfterEditingEvent = true;
  //  }
  //}

  onAppointmentAdding(e) {
    console.error('onAppointmentAdding fired');
    this.isFirstLoadAfterEditingEvent = false;
  }

  onAppointmentAdded(e) {
    console.debug("on appointment added invoked");
    this.showToast("Added", e.appointmentData.text, "success");

    console.debug('object to be added:');
    console.debug(e);

    let lessonStatusIndex = this.itemsLessonStatus.findIndex(x => x == e.appointmentData.lesson.lessonStatus);
    let attendeesArray: UserBasicDto[] = [];

    e.appointmentData.attendees.email.forEach(element => {
      let attendee: UserBasicDto = {
        email: element,
        firstName: '',
        lastName: '',
        phoneNumber: 0
      }
      attendeesArray.push(attendee);
    });

    let newEvent: Appointment = {
      subject: e.appointmentData.subject,
      startDate: e.appointmentData.startDate,
      endDate: e.appointmentData.endDate,
      isDone: e.appointmentData.isDone,
      isFreeSlot: e.appointmentData.isFreeSlot,
      description: e.appointmentData.description,
      calendarId: '',
      lesson: {
        title: e.appointmentData.lesson.title,
        lessonStatus: lessonStatusIndex,
        relatedInvoiceId: null,
        calendarEventId: 0,
      },
      attendees: attendeesArray,
    }

    this.data.createEvent(newEvent)
      .toPromise().then(success => {
        console.debug('success in createEvent')
        console.debug(success);

        this.data.loadEventsByDates(this.startViewDate, this.endViewDate)
            .toPromise().then(success => {
              console.debug(success);
              if (success) {
                this.appointmentsData = this.data.events;
              }
            });
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

    let lessonStatusIndex: number;
    if(typeof(e.appointmentData.lesson.lessonStatus) != 'number'){
      lessonStatusIndex = this.itemsLessonStatus.findIndex(x => x == e.appointmentData.lesson.lessonStatus);
    } else {
      lessonStatusIndex = e.appointmentData.lesson.lessonStatus
    }
    
    let attendeesArray: UserBasicDto[] = [];

    if(e.appointmentData.attendees.email == undefined){
      e.appointmentData.attendees.forEach(element => {  
        let attendee: UserBasicDto = {
          email: element.email,
          firstName: '',
          lastName: '',
          phoneNumber: 0
        }
        attendeesArray.push(attendee);
      });
    } else {
      e.appointmentData.attendees.email.forEach(element => {
          let attendee: UserBasicDto = {
            email: element,
            firstName: '',
            lastName: '',
            phoneNumber: 0
          }
          attendeesArray.push(attendee);
        });
    }

    let newEvent: Appointment = {
      subject: e.appointmentData.subject,
      startDate: e.appointmentData.startDate,
      endDate: e.appointmentData.endDate,
      isDone: e.appointmentData.isDone,
      isFreeSlot: e.appointmentData.isFreeSlot,
      description: e.appointmentData.description,
      calendarId: e.appointmentData.calendarId,// diff vs new event
      lesson: {
        title: e.appointmentData.lesson.title,
        lessonStatus: lessonStatusIndex,
        relatedInvoiceId: null,
        calendarEventId: 0,
      },
      attendees: attendeesArray,
    }

    this.data.updateEvent(newEvent)
      .toPromise().then(success => {
        console.debug('success in updateEvent')
        console.debug(success);

        this.data.loadEventsByDates(this.startViewDate, this.endViewDate)
            .toPromise().then(success => {
              console.debug(success);
              if (success) {
                this.appointmentsData = this.data.events;
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

    this.data.deleteEvent(e.appointmentData.calendarId)
      .toPromise().then(success => {
        console.debug('success in deleteEvent')
        console.debug(success);
    });

    // this.appointmentsData.load;
  }

  onAppointmentClick(e) {
    console.error('on appointment click fired');

    let externalCalendarId = e.appointmentData.calendarId;

    console.debug('externalCalendarId');
    console.debug(externalCalendarId);

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
          if (emailObjects.length !== 0) {
            emailObjects.forEach(
              (item) => {
                console.error('get lesson attendees - item email');
                console.debug(item.email);
                this.lessonEmails.push(item.email);
                console.debug(this.lessonEmails);
              });
          }
          console.debug('this.lessonEmails - final');
          console.debug(this.lessonEmails);
        }
      });
  }

  onAppointmentDoubleClick(e) {
    console.error('on appointment double click fired');
    this.onAppointmentClick(e); console.debug('onAppointmentClick');
    this.onAppointmentFormOpening(e); console.debug('onAppointmentFormOpening');
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
              dataField: "lesson.title",
              editorType: "dxTextArea",
              label: {
                text: "Lesson Title"
              },
              // editorOptions: {
              //   value: "lesson.title" //"Please add Lesson title"
              // }
            },
            {
              dataField: "lesson.lessonStatus",
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
              dataField: "attendees.email",
              editorType: "dxTagBox",
              label: {
                text: "Attendees' emails:"
              },
              editorOptions: {
                items: this.simpleEmails,
                searchEnabled: true,
                hideSelectedItems: true
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

    

    console.debug('e');
    console.debug(e);

    let emails: string[] = [];

    if (e.appointmentData.attendees != null){
            
      console.debug('emails1');
      console.debug(emails);

      //e.appointmentData.attendees.email.forEach(element => {
      e.appointmentData.attendees.forEach(element => {
        console.debug(element.email);
        emails.push(element.email as string);
      });

      console.debug('emails2');
      console.debug(emails);
    }
    
    if(e.appointmentData.lesson != undefined){
      e.form.itemOption("mainGroup").items[8].items[1].editorOptions.value =
        this.itemsLessonStatus[e.appointmentData.lesson.lessonStatus];
    }
    
    //Attendees
    let commonAttendees: string[] = this.simpleEmails.filter(value => emails.includes(value));
    console.debug('commonAttendees');
    console.debug(commonAttendees);
    e.form.itemOption("mainGroup").items[9].items[0].editorOptions.value = commonAttendees;

    e.form.itemOption("mainGroup.subject",
      {
        validationRules: [
          {
            type: "required",
            message: "Subject is required"
          }
        ]
      });
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
