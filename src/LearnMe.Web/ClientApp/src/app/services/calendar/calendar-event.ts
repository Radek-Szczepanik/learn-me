export interface CalendarEvent {
  //title: string;
  subject: string;
  description: string;
  startDate: Date;
  endDate: Date;
  isDone: boolean;
  isFreeSlot: boolean;
  calendarId: string;
}

export interface CalendarEventPost {
  subject: string;
  description: string;
  startDate: Date;
  endDate: Date;
  isDone: boolean;
  isFreeSlot: boolean;
  calendarId: string;
}
