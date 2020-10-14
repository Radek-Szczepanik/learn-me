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
export class CalendarViewComponent implements OnInit {

  appointmentsData: any;
  currentDate: Date = new Date(2020, 9, 14);
  //url = "https://localhost:44359/api/CalendarEvents?eventsPerPage=2&pageNumber=1";

  constructor(private data: CalendarService) {

    //this.appointmentsData = [{ title: "Codecool meeting - postponed to Friday", description: "Learn-me project sprint planning", startDate: "2020-10-16T06:00:00.671", endDate: "2020-10-16T08:00:00.671", isDone: false, calendarId: "oo913fskjsio7n3krp6lvhg1r4" }];

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
}
