using System;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using Microsoft.Extensions.Logging;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;
using LearnMe.Shared.Enum.Calendar;

namespace LearnMe.Core.Services.Calendar
{
    public class Calendar : ICalendar
    {
        private readonly ICrudRepository<CalendarEvent> _repository;
        private readonly IExternalCalendarService<Event> _externalCalendarService;
        private readonly ISynchronizer _synchronizer;
        private readonly IMapper _mapper;
        private readonly IEventBuilder _eventBuilder;
        private readonly ILogger<Calendar> _logger;

        public Calendar(
            ICrudRepository<CalendarEvent> repository,
            IExternalCalendarService<Event> externalCalendarService,
            ISynchronizer synchronizer,
            IMapper mapper,
            IEventBuilder eventBuilder,
            ILogger<Calendar> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _externalCalendarService = externalCalendarService ?? throw new ArgumentNullException(nameof(externalCalendarService));
            _synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventBuilder = eventBuilder ?? throw new ArgumentNullException(nameof(eventBuilder));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CalendarEventDto> CreateEventAsync(
            CalendarEventDto eventData,
            string calendarId = Constants.CalendarId,
            string timezone = Constants.Timezone,
            bool isRecurringEvent = false,
            Recurrence period = Recurrence.DAILY,
            int recurringEventsCount = 5,
            DateTime? recurUntilDateTime = null)
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

            var newCalendarEvent = await _externalCalendarService.InsertEventAsync(_eventBuilder.GetEvent());

            // Back to DB event
            newDbEvent.CalendarId = newCalendarEvent.Id;

            var insertedDbEvent = await _repository.InsertAsync(newDbEvent);

            if (insertedDbEvent != null)
            {
                return _mapper.Map<CalendarEventDto>(insertedDbEvent);
            } else
            {
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
            int pageNumber,
            string calendarId = Constants.CalendarId)
        {
            // Step 1 - synchronize Google calendar with DB
            await _synchronizer.SynchronizeDatabaseWithCalendarAsync(_externalCalendarService, _repository);

            // Step 2 - get all data from DB
            var eventsResult = await _repository.GetAllAsync(eventsPerPage, pageNumber);

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

        public async Task<bool> UpdateEventAsync(int id, CalendarEventDto eventData)
        {
            CalendarEvent toUpdateData = _mapper.Map<CalendarEvent>(eventData);
            toUpdateData.Id = id;

            var eventFromDbToUpdate = await _repository.GetByIdAsync(id);
            if (eventFromDbToUpdate != null)
            {
                toUpdateData.CalendarId = eventFromDbToUpdate.CalendarId;
                await _externalCalendarService.UpdateEventAsync(new Event()
                {
                    Summary = toUpdateData.Title
                    // TODO Populate event properties from argument data
                });
            }

            return await _repository.UpdateAsync(toUpdateData);
        }
    }
}
