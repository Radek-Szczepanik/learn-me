import { Component, Input, OnInit } from '@angular/core';
import { LessonAppointmentTableEntry } from '../../../services/calendar/calendar-service-ver-2';

@Component({
  selector: 'app-lesson-details',
  templateUrl: './lesson-details.component.html',
  styleUrls: ['./lesson-details.component.css']
})
export class LessonDetailsComponent implements OnInit {

  @Input()
  lesson: LessonAppointmentTableEntry;

  constructor() { }

  ngOnInit(): void {
  }

}
