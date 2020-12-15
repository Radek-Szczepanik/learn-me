import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { CalendarEvent } from "./calendar-event";
import { EventLesson, Lesson } from "../../Models/Lesson/lesson";
import * as Calendarevent from "./calendar-event";
import CalendarEventClass = Calendarevent.CalendarEventClass;

@Injectable()
export class CalendarService {

  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = '/api/';
  }

  public events: CalendarEvent[] = [];

  loadEventsByDates(from: Date, to: Date): Observable<CalendarEventClass[]> {
    let query = this.apiUrl + 'calendareventsbydate?fromDate=' + from.toJSON() + '&toDate=' + to.toJSON();

    let result: CalendarEventClass[] = [];

    return this.http.get(query)
      .pipe
      (map((data: CalendarEvent[]) => {
        data.forEach((item: CalendarEvent) => {
          let url = this.apiUrl + 'calendareventsbygoogleid/' + item.calendarId;
          this.loadEventWithLesson(url)
            .toPromise().then(success => {
              result.push(success as CalendarEventClass);
            });
        });
        return result;
      })
      
    );

  }

  // IT WORKS
  //loadEventsByDates(from: Date, to: Date): Observable<boolean> {
  //  let query = '/api/calendareventsbydate?fromDate=' + from.toJSON() + '&toDate=' + to.toJSON();

  //  return this.http.get(query)
  //    .pipe(
  //      map((data: CalendarEvent[]) => {
  //        this.events = data;
  //        console.debug('data - calendar service:');
  //        console.debug(data);
  //        console.debug('events - calendar service:');
  //        console.debug(this.events);
  //        return true;
  //      }));
  //}

  //loadEventsByDates$(from: Date, to: Date): Observable<CalendarEvent[]> {
  //  const query = '/api/calendareventsbydate?fromDate=' + from.toJSON() + '&toDate=' + to.toJSON();

  //  return this.http.get<CalendarEvent[]>(query);
  //}

  loadEventWithLesson(calendarEventQueryUrl: string): Observable<CalendarEventClass> {
    return this.http.get(calendarEventQueryUrl).pipe(
      mergeMap((calendarEvent: CalendarEvent) => {
        return this.http.get(this.apiUrl + 'lessons/' + calendarEvent.calendarId)
              .pipe(
                  map((lesson: Lesson) => {
                      return new CalendarEventClass(calendarEvent, lesson);
                  })
              );
      })
    );
  }
  //this.http.get(calendarEventUrl).pipe(
  //  mergeMap(calendarEvent => {
  //    this.http.get(apiUrl + calendarEvent.lessonId).pipe(
  //      map(lesson => {
  //        return new CalendarEvent(calendarEvent, lesson);
  //      })
  //    )
  //  })
  //);

  //this.homeworld = this.http
  //  .get('/api/people/1')
  //  .pipe(mergeMap(character => this.http.get(character.homeworld)));
}
