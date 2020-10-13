import { Component, OnInit } from '@angular/core';
import { CalendarService } from '../../services/calendar/calendar-service'
import { DxSchedulerModule } from 'devextreme-angular';
import { CalendarEvent } from '../../services/calendar/calendar-event';

import DataSource from 'devextreme/data/data_source';
import CustomStore from 'devextreme/data/custom_store';
import { HttpClient } from '@angular/common/http';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-calendar-view',
  templateUrl: './calendar-view.component.html',
  styleUrls: ['./calendar-view.component.css']
})
export class CalendarViewComponent {
  appointmentsData: any;
  currentDate: Date = new Date(2020, 9, 12);
  url = "https://localhost:44359/api/CalendarEvents?eventsPerPage=2&pageNumber=1";

  constructor(private http: HttpClient) {

    console.debug('getData output items:' + this.getData());
    this.appointmentsData = this.getData();
      //.then((data: any) => data.items);
    console.debug('appointmentsData:' + this.appointmentsData[0]);

//    this.appointmentsData = new DataSource({
//      store: new CustomStore({
//        load: () => this.getData();
//  })
//});

    //var url = "https://localhost:44359/api/CalendarEvents";

    //this.appointmentsData = AspNetData.createStore({
    //  //key: "CalendarId",
    //  loadUrl: url + "?eventsPerPage=100&pageNumber=1",
    //  //insertUrl: url + "/Post",
    //  //updateUrl: url + "/Put",
    //  //deleteUrl: url + "/Delete",
    //  //onBeforeSend: function (method, ajaxOptions) {
    //  //  ajaxOptions.xhrFields = { withCredentials: true };
    //  //}
    //});

    // WORKING EXAMPLE
    //this.appointmentsData = [
    //  {
    //    text: "Website Re-Design Plan",
    //    startDate: new Date(2020, 9, 12, 9, 30),
    //    endDate: new Date(2020, 9, 12, 11, 30)
    //  }, {
    //    text: "Book Flights to San Fran for Sales Trip",
    //    startDate: new Date(2020, 9, 14, 12, 0),
    //    endDate: new Date(2020, 9, 14, 13, 0),
    //  }
    //];

    //this.appointmentsData = [{
    //  movie: "His Girl Friday",
    //  price: 5,
    //  startDate: new Date(2020, 9, 12, 9, 30),
    //  endDate: new Date(2020, 9, 12, 11, 30)
    //}, {
    //  movie: "Royal Wedding",
    //  price: 10,
    //  startDate: new Date(2020, 9, 14, 12, 0),
    //  endDate: new Date(2020, 9, 14, 13, 0),
    //},
    //  {
    //    movie: "Codecool meeting",
    //    price: 5,
    //    "description": "Learn-me project sprint planning",
    //    startDate: new Date(2020, 9, 12, 12, 0),
    //    endDate: new Date(2020, 9, 12, 13, 0),
    //    "isDone": false,
    //    "calendarId": "t384f6d21dhh85epj4vt9c7io0"
    //  }
    //];

    //var url = "https://localhost:44359/api/CalendarEvents";

    //this.appointmentsData = AspNetData.createStore({
    //  //key: "CalendarId",
    //  loadUrl: url + "/181"
    //});

    //this.appointmentsData = [
      //{
      //  "title": "Codecool meeting hardcoded",
      //  "description": "Learn-me project sprint planning",
      //  "startDate": new Date(2020, 9, 12, 12, 0),
      //  "endDate": new Date(2020, 9, 12, 13, 0),
      //  "isDone": false,
      //  "calendarId": "t384f6d21dhh85epj4vt9c7io0"
      //},
      //{
      //  "title": "Codecool meeting",
      //  "description": "Learn-me project sprint planning",
      //  "startDate": "2020-09-12T08:00:00.671",
      //  "endDate": "2020-09-12T12:00:00.671",
      //  "isDone": false,
      //  "calendarId": "t384f6d21dhh85epj4vt9c7io0"
      //}
      //{
      //  "title": "Codecool meeting",
      //  "description": "Learn-me project sprint planning",
      //  "startDate": new Date("2020-10-12T14:00:00.671"),
      //  "endDate": new Date("2020-10-12T16:00:00.671"),
      //  "isDone": false,
      //  "calendarId": "t384f6d21dhh85epj4vt9c7io0"
      //}
    //];

    console.error('appointmentsData:');
    console.error(this.appointmentsData);
  }

  //private getData() {
  //  var dataUrl = "https://localhost:44359/api/CalendarEvents?eventsPerPage=2&pageNumber=1";
  //  console.debug('url:' + dataUrl.toString());
  //  return this.http.get(dataUrl).toPromise()
  //    .then((data: any) => data.items);
  //}
  //private getData() {
  //  var dataUrl = "https://localhost:44359/api/CalendarEvents?eventsPerPage=2&pageNumber=1";
  //  console.debug('url:' + dataUrl.toString());
  //  return this.http.get(dataUrl);
  //}
  private extractData(res: Response) {
    let body = res;
    return body || {};
  }

  public getData(url: string): Observable<any> {

    // Call the http GET
    return this.http.get(url).pipe(
      map(this.extractData)
    );
  }

  ngOnInit() {
    this.appointmentsData
      .getData(this.url)
      .subscribe(
        data => {
          console.debug('fromOnInitData' + data);
        },
        err => {
          console.error(err);
        }
      );
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
