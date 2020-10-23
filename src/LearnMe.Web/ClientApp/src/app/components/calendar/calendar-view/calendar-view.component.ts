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
  currentDate: Date = new Date(2020, 9, 16);
  timezone: string = "Europe/Warsaw";
  eventToAdd: CalendarEventPost;

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
  }

  ngOnInit(): void {
    this.data.loadEvents()
      .subscribe(success => {
        console.debug('is success in OnInit');
        console.debug(success);
        if (success) {
          this.appointmentsData = this.data.events;
        }
        console.debug('appointmentsData - after OnInit');
        console.debug(this.appointmentsData);
      });
  }

  showToast(event, value, type) {
    notify(event + " \"" + value + "\"" + " task", type, 800);
  }

  onAppointmentAdded(e) {
    console.debug("on appointment added invoked");
    console.debug(e.appointmentData.text);
    console.debug(e.appointmentData.subject);
    this.showToast("Added", e.appointmentData.text, "success");

    //let eventToAdd: CalendarEvent;

    this.eventToAdd.subject = e.appointmentData.text;
    this.eventToAdd.description = e.appointmentData.description;
    this.eventToAdd.startDate = e.appointmentData.startDate;
    this.eventToAdd.endDate = e.appointmentData.endDate;
    this.eventToAdd.isDone = false;
    this.eventToAdd.isFreeSlot = true;

    console.debug(this.eventToAdd);

    //this.data.addEvent(this.eventToAdd)
    //  .subscribe(success => {
    //    console.debug('is success in adding event');
    //    console.debug(success);
    //  });
    this.https.post('/api/calendarevents', this.eventToAdd)
      .subscribe(success => {
        if (success) {
          console.debug('event added to DB and Calendar');
        }
      });
    //.subscribe(success => {
    //  console.debug('is success in adding event');
    //  console.debug(success);
    //});
  }

  onAppointmentUpdated(e) {
    this.showToast("Updated", e.appointmentData.subject, "info");
  }

  onAppointmentDeleted(e) {
    this.showToast("Deleted", e.appointmentData.subject, "warning");
  }

  // z parent komponentu będzie dostęp do tego kalenarza
  // this.calendarApi = this.calendarComponent.getApi();
  // let currentDate = this.calendarApi.view.currentStart;
  // i current date przekazac z powrotem do child komponentu kalendarza

  onAppointmentFormOpening(e: any) {
    //const startDate = e.appointmentData.startDate;
    //if (!this.isValidAppointmentDate(startDate)) {
    //  e.cancel = true;
    //  this.notifyDisableDate();
    //}
    //this.applyDisableDatesToDateEditors(e.form);
  }

  onAppointmentAdding(eventToAdd: CalendarEvent) {

    //let eventToAdd: CalendarEvent;



    //eventToAdd.title;
    //eventToAdd.description
    //eventToAdd.startDate
    //eventToAdd.endDate
    //eventToAdd.isDone = false;
    //eventToAdd.isFreeSlot

    //this.data.addEvent(eventToAdd)
    //  .subscribe(success => {
    //    console.debug('is success in adding event');
    //    console.debug(success);
    //  });

    //const isValidAppointment = this.isValidAppointment(e.component, e.appointmentData);
    //if (!isValidAppointment) {
    //  e.cancel = true;
    //  this.notifyDisableDate();
    //}
  }

  onAppointmentUpdating(e: any) {
    //const isValidAppointment = this.isValidAppointment(e.component, e.newData);
    //if (!isValidAppointment) {
    //  e.cancel = true;
    //  this.notifyDisableDate();
    //}
  }
}
