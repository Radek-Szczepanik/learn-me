import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CalendarEvent } from "./calendar-event";

@Injectable()
export class CalendarService {
  constructor(private http: HttpClient) { }

  public events : CalendarEvent[] = [];

  loadEvents() : Observable<boolean> {
    return this.http.get('/api/calendarevents/181')
      .pipe(
        map((data: any[]) => {
          this.events = data;
          console.warn('data loaded from API:' + data);
          console.debug(data);
          return true;
        }));
  }
}
