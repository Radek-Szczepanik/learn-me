import { Injectable } from "@angular/core";
import { Lesson, UserBasicDto } from "../../Models/Lesson/lesson"

export class Appointment {
    // text: string;
    // startDate: Date;
    // endDate: Date;
    // allDay?: boolean;
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

// let appointments: Appointment[] = [
//     {
//         subject: "subject",
//         description: "description",
//         startDate: new Date("2020-12-28T10:30:00.000Z"),
//         endDate: new Date("2020-12-28T14:30:00.000Z"),
//         isDone: false,
//         isFreeSlot: false,
//         calendarId: "calendarId",
//         lesson: {
//             title: "lessonTitile1",
//             calendarEventId: 1,
//             lessonStatus: 0,
//             relatedInvoiceId: null
//         }
//     }, {
//         subject: "subject",
//         description: "description",
//         startDate: new Date("2020-12-29T11:30:00.000Z"),
//         endDate: new Date("2020-12-29T12:30:00.000Z"),
//         isDone: false,
//         isFreeSlot: true,
//         calendarId: "calendarId",
//         lesson: {
//             title: "lessonTitile2",
//             calendarEventId: 1,
//             lessonStatus: 1,
//             relatedInvoiceId: null
//         }
//     }, {
//         subject: "subject",
//         description: "description",
//         startDate: new Date("2020-12-30T09:30:00.000Z"),
//         endDate: new Date("2020-12-30T10:30:00.000Z"),
//         isDone: true,
//         isFreeSlot: true,
//         calendarId: "calendarId",
//         lesson: {
//             title: "lessonTitile3",
//             calendarEventId: 1,
//             lessonStatus: 2,
//             relatedInvoiceId: null
//         }
//     }
// ];

@Injectable()
export class Service {
    getAppointments(): Appointment[] {
        return appointments;
    }
}
