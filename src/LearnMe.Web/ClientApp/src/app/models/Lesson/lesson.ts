export interface Lesson
{
  title: string,
  lessonStatus: number,
  relatedInvoiceId: number,
  calendarEventId: number,
}

export enum LessonStatus {
    New,
    InProgress,
    Done
}

export interface EventLesson {
  subject: string;
  description: string;
  startDate: Date;
  endDate: Date;
  isDone: boolean;
  isFreeSlot: boolean;
  calendarId: string;
  title: string;
  lessonStatus: number;
  relatedInvoiceId: number;
  calendarEventId: number;
}

export interface AttendeeDto {
  attendeeEmail: string;
}

export interface UserBasicDto {
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: number;
}

export interface HomeworkDto {
  fileString: string;
  messageText: string;
}