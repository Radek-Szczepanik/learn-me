import { Component, OnInit } from '@angular/core';
import { CalendarService } from '../../services/calendar/calendar-service'
import { HttpClient } from '@angular/common/http';
import DataSource from 'devextreme/data/data_source';
import CustomStore from 'devextreme/data/custom_store';

@Component({
  selector: 'app-calendar-view',
  templateUrl: './calendar-view.component.html',
  styleUrls: ['./calendar-view.component.css']
})
export class CalendarViewComponent implements OnInit {

  appointmentsData: any;
  currentDate: Date = new Date(2020, 9, 14);

  constructor(private data: CalendarService) {
    console.debug('appointmentsData:');
    console.debug(this.appointmentsData);
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

  onAppointmentFormOpening(e: any) {
    //const startDate = e.appointmentData.startDate;
    //if (!this.isValidAppointmentDate(startDate)) {
    //  e.cancel = true;
    //  this.notifyDisableDate();
    //}
    //this.applyDisableDatesToDateEditors(e.form);
  }

  onAppointmentAdding(e: any) {

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
