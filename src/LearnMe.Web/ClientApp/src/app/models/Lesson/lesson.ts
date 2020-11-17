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

