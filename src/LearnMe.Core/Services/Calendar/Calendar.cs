using System;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils.Constants;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using Microsoft.Extensions.Logging;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository.Interfaces;
using LearnMe.Shared.Enum.Calendar;
using Microsoft.AspNetCore.Identity;

namespace LearnMe.Core.Services.Calendar
{
    public class Calendar : ICalendar
    {
        private readonly ICrudRepository<CalendarEvent> _repository;
        private readonly ICalendarEventsRepository _calendarEventsRepository;
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IExternalCalendarService<Event> _externalCalendarService;
        private readonly ICrudRepository<CalendarSynchronization> _synchronizationData;
        private readonly ICrudRepository<CalendarEvent> _eventsData;
        private readonly ISynchronizer _synchronizer;
        private readonly IMapper _mapper;
        private readonly IEventBuilder _eventBuilder;
        private readonly ILogger<Calendar> _logger;

        public Calendar(
            ICrudRepository<CalendarEvent> repository,
            ICalendarEventsRepository calendarEventsRepository,
            ILessonsRepository lessonsRepository,
            IExternalCalendarService<Event> externalCalendarService,
            ICrudRepository<CalendarSynchronization> synchronizationData,
            ICrudRepository<CalendarEvent> eventsData,
            ISynchronizer synchronizer,
            IMapper mapper,
            IEventBuilder eventBuilder,
            ILogger<Calendar> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _calendarEventsRepository = calendarEventsRepository ??
                                        throw new ArgumentNullException(nameof(calendarEventsRepository));
            _lessonsRepository = lessonsRepository ?? throw new ArgumentNullException(nameof(lessonsRepository));
            _externalCalendarService = externalCalendarService ?? throw new ArgumentNullException(nameof(externalCalendarService));
            _synchronizationData = synchronizationData ?? throw new ArgumentNullException(nameof(synchronizationData));
            _eventsData = eventsData ?? throw new ArgumentNullException(nameof(eventsData));
            _synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventBuilder = eventBuilder ?? throw new ArgumentNullException(nameof(eventBuilder));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // USED
        public async Task<FullCalendarEventDto> CreateFullEventAsync(
            FullCalendarEventDto eventData,
            string calendarId = CalendarConstants.CalendarId,
            string timezone = CalendarConstants.Timezone,
            bool isRecurringEvent = false,
            Recurrence period = Recurrence.DAILY,
            int recurringEventsCount = 5,
            DateTime? recurUntilDateTime = null,
            IList<string> attendeesEmails = null)
        {
            CalendarEvent newDbEvent = _mapper.Map<CalendarEvent>(eventData);

            // Google Event
            _eventBuilder.BuildBasicEventWithDescription(
                newDbEvent.Title,
                newDbEvent?.Start,
                newDbEvent?.End,
                newDbEvent.Description);

            if (isRecurringEvent)
            {
                _eventBuilder.SetRecurrence(period, recurringEventsCount, recurUntilDateTime);
            }

            attendeesEmails = new List<string>();
            foreach (var person in newDbEvent.Attendees)
            {
                attendeesEmails.Add(person.Email);
            }

            if (attendeesEmails.Count != 0)
            {
                foreach (var email in attendeesEmails)
                {
                    _eventBuilder.AddAttendee(email);
                }
            }

            _logger.LogDebug("Create event in Google - START");
            var newCalendarEvent = await _externalCalendarService.InsertEventAsync(_eventBuilder.GetEvent());
            _logger.LogDebug("Create event in Google - END");

            // Back to DB event
            newDbEvent.CalendarId = newCalendarEvent.Id;

            _logger.LogDebug("Create event in DB - START");
            //var insertedDbEvent = await _repository.InsertAsync(newDbEvent);
            var insertedDbEvent = await _calendarEventsRepository.InsertFullEventAsync(newDbEvent);
            _logger.LogDebug("Create event in DB - END");

            if (insertedDbEvent != null)
            {
                _logger.LogDebug("Create event ended");
                return _mapper.Map<FullCalendarEventDto>(insertedDbEvent);
            } else
            {
                _logger.LogDebug("Create event ended");
                return null;
            }
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventToBeDeleted = await _repository.GetByIdAsync(id);
            if (eventToBeDeleted != null)
            {
                await _externalCalendarService.DeleteEventAsync(eventToBeDeleted.CalendarId);
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CalendarEventDto>> GetAllEventsAsync(
            int eventsPerPage,
            int pageNumber)
        {
            //// Step 1 - synchronize Google calendar with DB
            //var eventsSynchronizedCount = await _synchronizer.SynchronizeDatabaseWithCalendarByDateModifiedAsync(
            //    _externalCalendarService,
            //    _calendarEventsRepository,
            //    _lessonsRepository,
            //    _synchronizationData,
            //    _eventsData);

            //_logger.Log(LogLevel.Debug, $"{DateTime.Now} Synchronized {eventsSynchronizedCount} events: from Calendar to DB");

            // Step 2 - get all data from DB
            var eventsResult = await _repository.GetAllWithPagination(eventsPerPage, pageNumber);

            if (eventsResult != null)
            {
                IList<CalendarEventDto> results = new List<CalendarEventDto>();
                foreach (var eventResult in eventsResult)
                {
                    results.Add(_mapper.Map<CalendarEventDto>(eventResult));
                }

                return results;
            }
            else
            {
                return null;
            }
        }

        public async Task<CalendarEventDto> GetEventByIdAsync(int id)
        {
            var foundEvent = await _repository.GetByIdAsync(id);

            return _mapper.Map<CalendarEventDto>(foundEvent);
        }

        public async Task<bool> UpdateEventAsync(
            int id,
            CalendarEventDto eventData,
            IList<string> attendeesEmails = null)
        {
            CalendarEvent toUpdateData = _mapper.Map<CalendarEvent>(eventData);
            toUpdateData.Id = id;

            var eventFromDbToUpdate = await _repository.GetByIdAsync(id);

            if (eventFromDbToUpdate != null)
            {
                toUpdateData.CalendarId = eventFromDbToUpdate.CalendarId;

                _eventBuilder.BuildBasicEventWithDescription(
                    toUpdateData.Title,
                    toUpdateData.Start,
                    toUpdateData.End,
                    toUpdateData.Description);

                if (attendeesEmails != null)
                {
                    foreach (var email in attendeesEmails)
                    {
                        _eventBuilder.AddAttendee(email);
                    }
                }

                await _externalCalendarService.UpdateEventAsync(
                    toUpdateData.CalendarId,
                    _eventBuilder.GetEvent());
            }

            return await _repository.UpdateAsync(toUpdateData);
        }

        public async Task<bool> UpdateEventByCalendarIdAsync(
            CalendarEventDto eventData,
            IList<string> attendeesEmails = null)
        {
            CalendarEvent toUpdateData = _mapper.Map<CalendarEvent>(eventData);

            var eventFromDbToUpdate =
                await _calendarEventsRepository.GetByCalendarIdAsync(eventData.CalendarId);

            toUpdateData.Id = eventFromDbToUpdate.Id;

            if (eventFromDbToUpdate != null)
            {
                _eventBuilder.BuildBasicEventWithDescription(
                    toUpdateData.Title,
                    toUpdateData.Start,
                    toUpdateData.End,
                    toUpdateData.Description);
                
                if (attendeesEmails != null)
                {
                    foreach (var email in attendeesEmails)
                    {
                        _eventBuilder.AddAttendee(email);
                    }
                }

                await _externalCalendarService.UpdateEventAsync(
                    eventFromDbToUpdate.CalendarId,
                    _eventBuilder.GetEvent());
            }

            return await _repository.UpdateAsync(toUpdateData);
        }

        // USED
        public async Task<bool> UpdateFullEventByCalendarIdAsync(
            FullCalendarEventDto eventData,
            IList<string> attendeesEmails = null)
        {
            CalendarEvent toUpdateData = _mapper.Map<CalendarEvent>(eventData);

            var eventFromDbToUpdate =
                await _calendarEventsRepository.GetFullEventByCalendarIdAsync(eventData.CalendarId);

            toUpdateData.Id = eventFromDbToUpdate.Id;

            if (eventFromDbToUpdate != null)
            {
                _eventBuilder.BuildBasicEventWithDescription(
                    toUpdateData.Title,
                    toUpdateData.Start,
                    toUpdateData.End,
                    toUpdateData.Description);

                attendeesEmails = new List<string>();
                foreach (var person in toUpdateData.Attendees)
                {
                    attendeesEmails.Add(person.Email);
                }

                if (attendeesEmails.Count != 0)
                {
                    _eventBuilder.UpdateAttendees(attendeesEmails);
                }
                else
                {
                    _eventBuilder.RemoveAllAttendees();
                }

                await _externalCalendarService.UpdateEventAsync(
                    eventFromDbToUpdate.CalendarId,
                    _eventBuilder.GetEvent());
            }

            return await _calendarEventsRepository.UpdateFullEventByCalendarIdAsync(eventFromDbToUpdate.CalendarId, toUpdateData);
        }

        public async Task<bool> DeleteEventByCalendarIdAsync(string calendarId)
        {
            var result = false;

            var eventToBeDeleted =
                await _calendarEventsRepository.GetByCalendarIdAsync(calendarId);
            if (eventToBeDeleted != null)
            {
                await _externalCalendarService.DeleteEventAsync(eventToBeDeleted.CalendarId);
                result = await _repository.DeleteAsync(eventToBeDeleted.Id);
            }

            return result;
        }

        // USED
        public async Task<bool> DeleteFullEventByCalendarIdAsync(string calendarId)
        {
            var result = false;

            var eventToBeDeleted =
                await _calendarEventsRepository.GetFullEventByCalendarIdAsync(calendarId);
            if (eventToBeDeleted != null)
            {
                await _externalCalendarService.DeleteEventAsync(eventToBeDeleted.CalendarId);
                result = await _repository.DeleteAsync(eventToBeDeleted.Id);
            }

            return result;
        }

        public async Task<IEnumerable<FullCalendarEventDto>> GetFullEventsByUserRoleByDatesAsync(
            string userRole,
            string userEmail,
            DateTime fromDate,
            DateTime toDate)
        {
            // Step 1 - synchronize Google calendar with DB
            //var eventsSynchronizedCount = await _synchronizer.SynchronizeDatabaseWithCalendarByDateModifiedAsync(
            //    _externalCalendarService,
            //    _calendarEventsRepository,
            //    _lessonsRepository,
            //    _synchronizationData,
            //    _eventsData);

            //_logger.Log(LogLevel.Debug, $"{DateTime.Now} Synchronized {eventsSynchronizedCount} events: from Calendar to DB");

            // Step 2 - get all data from DB
            var eventsResult = await _calendarEventsRepository.GetFullEventForRoleByFromAndToDateAsync(
                userRole,
                userEmail,
                fromDate,
                toDate);

            if (eventsResult != null)
            {
                IList<FullCalendarEventDto> results = new List<FullCalendarEventDto>();
                foreach (var eventResult in eventsResult)
                {
                    results.Add(_mapper.Map<FullCalendarEventDto>(eventResult));
                }

                return results;
            } else
            {
                return null;
            }
        }
    }
}
