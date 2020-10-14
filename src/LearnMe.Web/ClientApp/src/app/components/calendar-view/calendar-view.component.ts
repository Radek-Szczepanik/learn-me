import { Component, OnInit } from '@angular/core';
import { CalendarService } from '../../services/calendar/calendar-service'

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
}
