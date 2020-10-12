import { Component, OnInit } from '@angular/core';
import { CalendarService } from '../../services/calendar/calendar-service'
import { DxSchedulerModule } from 'devextreme-angular';
import { CalendarEvent } from '../../services/calendar/calendar-event';

import DataSource from 'devextreme/data/data_source';
import CustomStore from 'devextreme/data/custom_store';
import { HttpClient } from '@angular/common/http';
import * as AspNetData from "devextreme-aspnet-data-nojquery";

@Component({
  selector: 'app-calendar-view',
  templateUrl: './calendar-view.component.html',
  styleUrls: ['./calendar-view.component.css']
})
export class CalendarViewComponent {
  appointmentsData: any;
  currentDate: Date = new Date(2020, 10, 12);

  constructor() {
    var url = "https://js.devexpress.com/Demos/Mvc/api/SchedulerData";

    this.appointmentsData = AspNetData.createStore({
      key: "AppointmentId",
      loadUrl: url + "/Get",
      insertUrl: url + "/Post",
      updateUrl: url + "/Put",
      deleteUrl: url + "/Delete",
      onBeforeSend: function (method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
      }
    });
  }
}
//export class CalendarViewComponent implements OnInit {

//  dataSource: any;

//  constructor(private data: CalendarService, private http: HttpClient) {
//    this.dataSource = new DataSource({
//      store: new CustomStore({
//        load: (options) => this.getData(options, {})
//      })
//    });
//    console.warn('dataSource :' + this.dataSource);
//    console.debug(this.dataSource);
//    //this.events = data.events;
//    //console.warn('events :' + data.events);
//    //console.debug(data.events);

//    this.data.loadEvents()
//      .subscribe(success => {
//        if (success) {
//          this.events = this.data.events;
//        }
//      });

//    this.events = data.events;
//    console.warn('events :' + this.events);
//    console.debug(this.events);
//  }

//  //private getData(options: any, requestOptions: any) {

//  //  return this.http.get('/api/calendarevents?eventsPerPage=50&pageNumber=1', requestOptions).toPromise().then((data: any) => data.items);
//  //}

//  private getData(options: any, requestOptions: any) {
//    let PUBLIC_KEY = 'AIzaSyBnNAISIUKe6xdhq1_rjor2rxoI3UlMY7k',
//      CALENDAR_ID = 'f7jnetm22dsjc3npc2lu3buvu4@group.calendar.google.com';
//    let dataUrl = ['https://www.googleapis.com/calendar/v3/calendars/',
//      CALENDAR_ID, '/events?key=', PUBLIC_KEY].join('');

//    return this.http.get(dataUrl, requestOptions).toPromise().then((data: any) => data.items);
//  }

//  public events = [];

//  ngOnInit(): void {
//    this.data.loadEvents()
//      .subscribe(success => {
//        if (success) {
//          this.events = this.data.events;
//        }
//      });
//  }
//}
