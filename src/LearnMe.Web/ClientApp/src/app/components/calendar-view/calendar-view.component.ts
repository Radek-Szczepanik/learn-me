import { Component, OnInit } from '@angular/core';
import { CalendarService } from "../../services/calendar/calendar-service"

//import { NgModule, Component, Inject, enableProdMode } from '@angular/core';
//import { HttpClient, HttpClientModule } from '@angular/common/http';
import { DxSchedulerModule } from 'devextreme-angular';

//import DataSource from 'devextreme/data/data_source';

//if (!/localhost/.test(document.location.host)) {
//  enableProdMode();
//}

@Component({
  selector: 'app-calendar-view',
  templateUrl: './calendar-view.component.html',
  styleUrls: ['./calendar-view.component.css']
})
export class CalendarViewComponent implements OnInit {

  constructor(private data: CalendarService) {
    this.events = data.events;
  }

  public events = [];

  ngOnInit(): void {
    this.data.loadEvents()
      .subscribe(success => {
        if (success) {
          this.events = this.data.events;
        }
      });
  }
  //  dataSource: any;
  //  currentDate: Date = new Date(2017, 4, 25);

  //  constructor(private http: HttpClient) {
  //  }
}
