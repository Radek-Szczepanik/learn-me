export interface CalendarEvent {
  title: string;
  description: string;
  startDate: Date;
  endDate: Date;
  isDone: boolean;
  isFreeSlot: boolean;
  calendarId: string;
}
