import { Lesson } from "../../Models/Lesson/lesson";

export interface CalendarEvent {
  //title: string;
  subject: string;
  description: string;
  startDate: Date;
  endDate: Date;
  isDone: boolean;
  isFreeSlot: boolean;
  calendarId: string;
  lesson: Lesson;
}

export class CalendarEventClass {
  //title: string;
  subject: string;
  description: string;
  startDate: Date;
  endDate: Date;
  isDone: boolean;
  isFreeSlot: boolean;
  calendarId: string;
  lesson: Lesson;

  constructor(calEvent: CalendarEvent, lesson: Lesson) {
    this.subject = calEvent.subject;
    this.description = calEvent.description;
    this.startDate = calEvent.startDate;
    this.endDate = calEvent.endDate;
    this.isDone = calEvent.isDone;
    this.isFreeSlot = calEvent.isFreeSlot;
    this.calendarId = calEvent.calendarId;
    this.lesson = lesson;
  }
}

export interface CalendarEventPost {
  //title: string;
  subject: string;
  description: string;
  startDate: Date;
  endDate: Date;
  isDone: boolean;
  isFreeSlot: boolean;
  calendarId: string;
}
