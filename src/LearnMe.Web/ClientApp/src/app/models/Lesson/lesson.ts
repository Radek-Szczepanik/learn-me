export interface Lesson
{
 title: string,
 lessonStatus: LessonStatus,
 calendarEventId: number
}

export enum LessonStatus {
    New,
    InProgress,
    Done
}

