"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CalendarEventClass = void 0;
var CalendarEventClass = /** @class */ (function () {
    function CalendarEventClass(calEvent, lesson) {
        this.subject = calEvent.subject;
        this.description = calEvent.description;
        this.startDate = calEvent.startDate;
        this.endDate = calEvent.endDate;
        this.isDone = calEvent.isDone;
        this.isFreeSlot = calEvent.isFreeSlot;
        this.calendarId = calEvent.calendarId;
        this.lesson = lesson;
    }
    return CalendarEventClass;
}());
exports.CalendarEventClass = CalendarEventClass;
//# sourceMappingURL=calendar-event.js.map