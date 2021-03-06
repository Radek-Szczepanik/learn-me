﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Domains.Calendar;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface ICalendarEventsRepository : ICrudRepository<CalendarEvent>
    {
        Task<bool> DeleteByCalendarIdAsync(string calendarId);

        Task<CalendarEvent> GetByCalendarIdAsync(string calendarId);

        Task<IEnumerable<CalendarEvent>> GetByFromAndToDate(
            DateTime fromDate,
            DateTime toDate);

        Task<CalendarEvent> GetFullEventByCalendarIdAsync(string calendarId);

        Task<IEnumerable<CalendarEvent>> GetFullEventByFromAndToDateAsync(
            DateTime fromDate,
            DateTime toDate);

        Task<IEnumerable<CalendarEvent>> GetFullEventForRoleByFromAndToDateAsync(
            string roleName,
            string userEmail,
            DateTime fromDate,
            DateTime toDate);

        Task<bool> UpdateByCalendarIdAsync(
            string calendarId,
            string summary,
            string description,
            DateTime? startDateTime,
            DateTime? endDateTime);

        Task<bool> UpdateFullEventByCalendarIdAsync(
            string calendarId,
            CalendarEvent fullEvent);

        Task<CalendarEvent> InsertFullEventAsync(CalendarEvent fullEvent);
    }
}
