import { Component, OnInit } from '@angular/core';
import { CalendarService } from '../../../services/calendar/calendar-service'
import * as Calendarevent from "../../../services/calendar/calendar-event";
import CalendarEvent = Calendarevent.CalendarEvent;
import notify from 'devextreme/ui/notify';
import { HttpService } from "../../../services/http.service";
import CalendarEventPost = Calendarevent.CalendarEventPost;
import Scheduler from "devextreme/ui/scheduler";
import { Lesson, LessonStatus, EventLesson } from '../../../Models/Lesson/lesson'

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
  appointmentsAndLessonsData: EventLesson[];
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

  itemsLessonStatus: string[] = ["New", "InProgress", "Done"];

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
  }

  ngOnInit(): void { console.error('ngOnInit fired'); }

  onInitialized(e) {
    console.error('onInitialized fired');
  }

  onContentReady(e) {
    console.debug('on content ready fired!');
    this.getCalendarCurrentDate();

    if (this.isFirstLoadFlag && this.isFirstLoadAfterEditingEvent) {
      console.error('onContentReady isFirstLoadFlag');

      this.data.loadEventsByDates(this.startViewDate, this.endViewDate)
        .toPromise().then(success => {
          console.debug(success);
          if (success) {
            this.appointmentsAndLessonsData = this.data.events;
          }
          console.debug('appointmentsData - onContentReady');
          console.debug(this.appointmentsAndLessonsData);
        });

      //if (this.appointmentsAndLessonsData != undefined) {
      //  this.appointmentsAndLessonsData.forEach((item) => {
      //    let externalCalendarId = item.calendarId;

      //    let route = '/api/lessons/' + externalCalendarId;

      //    this.https.getData(route)
      //      .toPromise().then(success => {
      //        if (success) {
      //          let lesson = success as Lesson;
      //          this.lessons.push(lesson);

      //          let itemIndex = this.appointmentsAndLessonsData.findIndex(x => x.calendarId == item.calendarId);
      //          this.appointmentsAndLessonsData[itemIndex].title = lesson.title;
      //          this.appointmentsAndLessonsData[itemIndex].lessonStatus = lesson.lessonStatus;
      //        }
      //      });
      //    });
      //}
      

      console.debug('lessons after load');
      console.debug(this.lessons);

      this.isFirstLoadFlag = false;
      this.isFirstLoadAfterEditingEvent = false;

    } else {
      console.error('onContentReady not 1st load');

      this.isFirstLoadFlag = true;
      this.isFirstLoadAfterEditingEvent = true;
    }
  }

  showToast(event, value, type) {
    notify(event + " \"" + value + "\"" + " task", type, 1800);
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
            .subscribe(success => {
              if (success) {
                console.debug('lesson added to DB');
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
      .subscribe(success => {
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
          // ----

          e.appointmentData.title = lesson.title;
          e.appointmentData.lessonStatus = lesson.lessonStatus;
          console.debug('e.appointmentData');
          console.debug(e.appointmentData);

          console.error('form items investigation');
          console.debug(e.form.itemOption("mainGroup").items[8].items[0].editorOptions.value);
          console.debug(e.form.itemOption("mainGroup").items[8].items[1].editorOptions.value);

          //e.form.itemOption("mainGroup").items[8].items[0].editorOptions.value = lesson.title;
          //e.form.itemOption("mainGroup").items[8].items[1].editorOptions.value = lesson.lessonStatus;
        }
      });
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
        });

      e.form.itemOption("mainGroup",
        {
          items: formItems
        });

      this.appointmentFormUpdatedFlag = true;
    }

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

    let externalCalendarId = e.appointmentData.calendarId;

    let route = '/api/lessons/' + externalCalendarId;

    this.https.getData(route)
      .toPromise().then(success => {
        if (success) {
          console.debug('lesson fetched from DB');
          console.debug(success);
          let lesson = success as Lesson;
          e.appointmentData.title = lesson.title;
          e.appointmentData.lessonStatus = lesson.lessonStatus;
          console.debug('e.appointmentData');
          console.debug(e.appointmentData);

          console.error('form items investigation');
          console.debug(e.form.itemOption("mainGroup").items[8].items[0].editorOptions.value);
          console.debug(e.form.itemOption("mainGroup").items[8].items[1].editorOptions.value);

          //e.form.itemOption("mainGroup").items[8].items[0].editorOptions.value = lesson.title;
          //e.form.itemOption("mainGroup").items[8].items[1].editorOptions.value = lesson.lessonStatus;
          e.form.itemOption("mainGroup").items[8].items[0].editorOptions.value = this.currentLesson.title;
          e.form.itemOption("mainGroup").items[8].items[1].editorOptions.value = this.currentLesson.lessonStatus;
        }
      });

    console.debug('test get items');
    console.debug(e.form.itemOption("mainGroup").items[8]);
  }

  async delay(ms: number) {
    await new Promise(resolve => setTimeout(()=>resolve(), ms)).then(()=>console.log("fired"));
  }

  getCalendarCurrentDate() {
    let element = document.getElementById("myScheduler");
    let instance = Scheduler.getInstance(element) as Scheduler;

    this.startViewDate = instance.getStartViewDate();
    this.endViewDate = instance.getEndViewDate();
  }
}
