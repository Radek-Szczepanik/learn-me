import { Injectable } from "@angular/core";
import { Lesson, UserBasicDto, HomeworkDto } from "../../models/Lesson/lesson"

export class Appointment {
    subject: string;
    description: string;
    startDate: Date;
    endDate: Date;
    isDone: boolean;
    isFreeSlot: boolean;
    calendarId: string;
    lesson: Lesson;
    attendees: UserBasicDto[];
}

export class LessonAppointmentTableEntry {
    subject: string;
    description: string;
    startDate: Date;
    startDateTime: Date;
    endDateTime: Date;
    isDone: boolean;
    isFreeSlot: boolean;
    calendarId: string;
    lesson: Lesson;
    attendees: UserBasicDto[];
    attendeesNameAndSurnameList: string[];
    relatedMaterials: HomeworkDto[];
}

export interface Tile {
    color: string;
    cols: number;
    rows: number;
    text: string;
  }

@Injectable()
export class Service {
    getAppointments(): Appointment[] {
        // return appointments;
        return [];
    }
}
