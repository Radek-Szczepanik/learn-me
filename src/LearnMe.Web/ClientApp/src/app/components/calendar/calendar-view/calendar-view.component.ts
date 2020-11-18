import { Component, OnInit } from '@angular/core';
import { CalendarService } from '../../../services/calendar/calendar-service'
import * as Calendarevent from "../../../services/calendar/calendar-event";
import CalendarEvent = Calendarevent.CalendarEvent;
import notify from 'devextreme/ui/notify';
import { HttpService } from "../../../services/http.service";
import CalendarEventPost = Calendarevent.CalendarEventPost;
import Scheduler from "devextreme/ui/scheduler";
import DataSource from 'devextreme/data/data_source';
import { Lesson, LessonStatus } from '../../../Models/Lesson/lesson'

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
  lessonsData: Lesson[];
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

  //itemsLessonStatus: object[] = [{ id:1, value: "New" }, { id:2, value: "InProgress" }, { id:3, value: "Done" }];
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
  }

  ngOnInit(): void { console.error('ngOnInit fired'); }

  onInitialized(e) {
    console.error('onInitialized fired');
  }

  onContentReady(e) {
    //console.error('onContentReady fired');

    console.debug('on content ready fired!');
    this.getCalendarCurrentDate();

    if (this.isFirstLoadFlag && this.isFirstLoadAfterEditingEvent) {
      console.error('onContentReady isFirstLoadFlag');
      this.data.loadEventsByDates(this.startViewDate, this.endViewDate)
        .toPromise().then(success => {
          console.debug(success);
          if (success) {
            this.appointmentsData = this.data.events;
          }
          console.debug('appointmentsData - onContentReady');
          console.debug(this.appointmentsData);
        });

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

    let deleteUrl = '/api/calendareventsbygoogleid/' + e.appointmentData.calendarId;

    this.https.delete(deleteUrl)
      .toPromise().then(success => {
        if (success) {
          console.debug('event deleted from DB and Calendar');
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
                value: "Please add Lesson title"
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

    // for (var i = 0; i < 10; i++) {
    //  let formItems2 = e.form.itemOption("recurrenceGroup").items;
    //  console.debug(formItems2);
    //   await this.delay(1000);
    //}

    let externalCalendarId = e.appointmentData.calendarId;

    let route = '/api/lessons/' + externalCalendarId;

    //this.https.getData(route)
    //  .subscribe(success => {
    //    if (success) {
    //      console.debug('lesson fetched from DB');
    //      console.debug(success);
    //      let lesson = success as Lesson;
    //      e.appointmentData.title = lesson.title;
    //      //let lessonStatusIndex = this.itemsLessonStatus.findIndex(x => x == lesson.lessonStatus.toString());
    //      //e.appointmentData.lessonStatus = this.itemsLessonStatus[lessonStatusIndex];
    //      e.appointmentData.lessonStatus = lesson.lessonStatus;
    //    }
    //  });

    this.https.getData(route)
      .toPromise().then(success => {
        if (success) {
          console.debug('lesson fetched from DB');
          console.debug(success);
          let lesson = success as Lesson;
          e.appointmentData.title = lesson.title;
          //let lessonStatusIndex = this.itemsLessonStatus.findIndex(x => x == lesson.lessonStatus.toString());
          //e.appointmentData.lessonStatus = this.itemsLessonStatus[lessonStatusIndex];
          e.appointmentData.lessonStatus = lesson.lessonStatus;
          console.debug('e.appointmentData');
          console.debug(e.appointmentData);

          e.form.itemOption("mainGroup").items[8].title = lesson.title;
          e.form.itemOption("mainGroup").items[8].lessonStatus = lesson.lessonStatus;
        }
      });

    //console.debug('e.form.itemOption("Lesson Data.title")');
    //console.debug(e.form.itemOption("Lesson Data.title"));
    //e.form.itemOption("Lesson Data.title",
    //  {
    //    value: e.appointmentData.title
    //  });

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
