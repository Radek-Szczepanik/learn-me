import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  getCalendarCurrentDate() {
    //this.calendarApi = this.calendarComponent.getApi();
    //let currentDate = this.calendarApi.view.currentStart;
  }
}
