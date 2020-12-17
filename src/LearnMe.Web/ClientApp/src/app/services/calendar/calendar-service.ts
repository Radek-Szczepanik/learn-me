import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CalendarEvent } from './calendar-event';
import { EventLesson } from '../../models/Lesson/lesson';

@Injectable()
export class CalendarService {
  constructor(private http: HttpClient) { }

  public events: CalendarEvent[] = [];

  //loadEvents() : Observable<boolean> {
    //return this.http.get('/api/calendarevents?eventsPerPage=100&pageNumber=1')
    //  .pipe(
    //    map((data: CalendarEvent[]) => {
    //      this.events = data;
    //      console.debug('data - calendar service:');
    //      console.debug(data);
    //      console.debug('events - calendar service:');
    //      console.debug(this.events);
    //      return true;
    //    }));
  //}

  loadEventsByDates(from: Date, to: Date): Observable<boolean> {
    let query = '/api/calendareventsbydate?fromDate=' + from.toJSON() + '&toDate=' + to.toJSON();

    return this.http.get(query)
      .pipe(
        map((data: CalendarEvent[]) => {
          this.events = data;
          console.debug('data - calendar service:');
          console.debug(data);
          console.debug('events - calendar service:');
          console.debug(this.events);
          return true;
        }));
  }
}
