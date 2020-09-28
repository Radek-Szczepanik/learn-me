﻿using System;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using Microsoft.Extensions.Logging;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Core.Services.Calendar
{
    public class GoogleCalendar : ICalendar
    {
        //private readonly CalendarService _calendarService;
        private readonly ICalendarService _calendarService;
        //private readonly string _applicationName = Constants.ApplicationName;
        private readonly ICrudRepository<CalendarEvent> _repository;
        private readonly ISynchronizer _synchronizer;
        private readonly IGoogleCRUD _googleCrudAccess;
        private readonly IMapper _mapper;
        private readonly ILogger<GoogleCalendar> _logger;

        public GoogleCalendar(
            ICalendarService calendarService,
            IGoogleAPIconnection googleAPIconnection, 
            ICrudRepository<CalendarEvent> repository, 
            ISynchronizer synchronizer, 
            IGoogleCRUD googleCrudAccess, 
            IMapper mapper, 
            ILogger<GoogleCalendar> logger)
        {
            // TODO Remove asynchronous operations from constructor
            //var token = googleAPIconnection.GetToken().Result;
            //_calendarService = googleAPIconnection.CreateCalendarService(token, _applicationName);
            _calendarService = calendarService;

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            _googleCrudAccess = googleCrudAccess ?? throw new ArgumentNullException(nameof(googleCrudAccess));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //TODO Add Event builder implementation
        public async Task<bool> CreateEventAsync(
            CalendarEventDto eventData, 
            string calendarId = Constants.CalendarId, 
            string timezone = Constants.Timezone, 
            bool isRecurringEvent = false)
        {
            CalendarEvent newDbEvent = _mapper.Map<CalendarEvent>(eventData);

            Event newCalendarEvent = new Event()
            {
                Summary = newDbEvent.Title,
                Description = newDbEvent.Description
            };

            DateTime? startDateTime = newDbEvent?.Start;
            EventDateTime start = new EventDateTime()
            {
                DateTime = startDateTime,
                TimeZone = timezone
            };
            newCalendarEvent.Start = start;

            DateTime? endDateTime = newDbEvent?.End;
            EventDateTime end = new EventDateTime()
            {
                DateTime = endDateTime,
                TimeZone = timezone
            };
            newCalendarEvent.End = end;

            if (isRecurringEvent)
            {
                String[] recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=7" };
                newCalendarEvent.Recurrence = recurrence.ToList();
            }

            // FOR FUTURE - ADD ATTENDEES
            //EventAttendee[] attendees = new EventAttendee[]
            //{
            //    new EventAttendee() { Email = "<---email-address--->@gmail.com" },
            //};
            //newEvent.Attendees = attendees.ToList();

            // TODO Uncomment after tests
            //var createdEvent = await _calendarService.Events.Insert(newCalendarEvent, calendarId).ExecuteAsync();
            //newDbEvent.CalendarId = createdEvent.Id;

            return await _repository.InsertAsync(newDbEvent);
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CalendarEventDto>> GetAllEventsAsync(string calendarId = Constants.CalendarId)
        {
            // Step 1 - synchronize Google calendar with DB
            //await _synchronizer.SynchronizeDatabaseWithCalendarAsync(_googleCrudAccess, _calendarService, _repository);

            // Step 2 - get all data from DB
            var eventsResult = await _repository.GetAllAsync();

            IList<CalendarEventDto> results = new List<CalendarEventDto>();
            foreach (var eventResult in eventsResult)
            {
                results.Add(_mapper.Map<CalendarEventDto>(eventResult));
            }

            return results;
        }

        public async Task<CalendarEventDto> GetEventByIdAsync(int id)
        {
            var foundEvent = await _repository.GetByIdAsync(id);

            return _mapper.Map<CalendarEventDto>(foundEvent);
        }

        public async Task<bool> UpdateEventAsync(int id, CalendarEventDto eventData)
        {
            CalendarEvent toUpdateData = _mapper.Map<CalendarEvent>(eventData);
            toUpdateData.Id = id;

            var eventFromDbToUpdate = _repository.GetByIdAsync(id).Result;
            toUpdateData.CalendarId = eventFromDbToUpdate.CalendarId;

            return await _repository.UpdateAsync(toUpdateData);
        }
    }
}
