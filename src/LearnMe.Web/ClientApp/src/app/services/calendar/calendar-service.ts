import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable()
export class CalendarService {
  constructor(private http: HttpClient) { }

  public events = [];

  loadEvents() {
    return this.http.get('/api/calendarevents')
      .pipe(
        map((data: any[]) => {
          this.events = data;
          return true;
        }));
  }
}
