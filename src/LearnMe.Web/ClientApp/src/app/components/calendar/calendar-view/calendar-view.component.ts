import { Component, OnInit } from '@angular/core';
import { CalendarService } from '../../../services/calendar/calendar-service'
import { HttpClient } from '@angular/common/http';
import DataSource from 'devextreme/data/data_source';
import CustomStore from 'devextreme/data/custom_store';
import * as Calendarevent from "../../../services/calendar/calendar-event";
import CalendarEvent = Calendarevent.CalendarEvent;
import notify from 'devextreme/ui/notify';
import { HttpService } from "../../../services/http.service";
import CalendarEventPost = Calendarevent.CalendarEventPost;
import Scheduler from "devextreme/ui/scheduler";

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
  currentDate: Date = new Date();
  timezone: string = "Europe/Warsaw";
  eventToAdd: CalendarEventPost;
  appointmentFormUpdatedFlag: boolean = false;
  isFirstLoadFlag: boolean = true;
  dataLoaded: boolean = false;

  startViewDate: Date;
  endViewDate: Date;

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

    //let maxDate: Date = new Date();
    //maxDate.setDate(maxDate.getDate() + 7); // fetches 1 week (= 7 days) from current date

    //this.data.loadEventsByDates(this.currentDate, maxDate)
    //  .subscribe(success => {
    //    console.debug('is success in OnInit');
    //    console.debug(success);
    //    if (success) {
    //      this.appointmentsData = this.data.events;
    //    }
    //    console.debug('appointmentsData - after OnInit');
    //    console.debug(this.appointmentsData);
    //  });
  }

  ngOnInit(): void {}

  onInitialized(e) {
    console.debug("on initialized fired!");
  }

  onContentReady(e) {
    console.debug('on content ready fired!');
    this.getCalendarCurrentDate();

    if (this.isFirstLoadFlag) {
      this.data.loadEventsByDates(this.startViewDate, this.endViewDate)
        .subscribe(success => {
          console.debug('is success in OnInit');
          console.debug(success);
          if (success) {
            this.appointmentsData = this.data.events;
          }
          console.debug('appointmentsData - after OnInit');
          console.debug(this.appointmentsData);
        });

      this.isFirstLoadFlag = false;

    } else {

      this.isFirstLoadFlag = true;

    }
  }

  showToast(event, value, type) {
    notify(event + " \"" + value + "\"" + " task", type, 800);
  }

  onAppointmentAdded(e) {
    console.debug("on appointment added invoked");
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

    this.https.post('/api/calendarevents', this.eventToAdd)
      .subscribe(success => {
        if (success) {
          console.debug('event added to DB and Calendar');
        }
      });
  }

  onAppointmentUpdated(e) {
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
      .subscribe(success => {
        if (success) {
          console.debug('event updated in DB and Calendar');
        }
      });
  }

  onAppointmentDeleted(e) {
    this.showToast("Deleted", e.appointmentData.subject, "warning");

    console.debug('when deleted object is:');
    console.debug(e);

    let deleteUrl = '/api/calendareventsbygoogleid/' + e.appointmentData.calendarId;

    this.https.delete(deleteUrl)
      .subscribe(success => {
        if (success) {
          console.debug('event deleted from DB and Calendar');
        }
      });
  }

  onAppointmentFormOpening(e) {
    console.debug("onAppointmentFormOpening fired!");

    if (!this.appointmentFormUpdatedFlag) {
      let formItems = e.form.itemOption("mainGroup").items;

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
        });

      e.form.itemOption("mainGroup",
        {
          items: formItems
        });

      e.form.itemOption("mainGroup.subject",
        {
          validationRules: [
            {
              type: "required",
              message: "Subject is required"
            }
          ]
        });

      this.appointmentFormUpdatedFlag = true;
    }

    console.debug(e.form.itemOption("mainGroup").items);
    console.debug(this.appointmentFormUpdatedFlag);
  }

  getCalendarCurrentDate() {
    let element = document.getElementById("myScheduler");
    let instance = Scheduler.getInstance(element) as Scheduler;
    console.debug("scheduler instance");
    console.debug(instance);
    console.debug("getting start view date");
    let startViewDate = instance.getStartViewDate();
    console.debug("start view date - value:");
    console.debug(startViewDate);

    this.startViewDate = instance.getStartViewDate();
    this.endViewDate = instance.getEndViewDate();
  }
}
