using System;
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
using LearnMe.Shared.Enum.Calendar;

namespace LearnMe.Core.Services.Calendar
{
    public class GoogleCalendar : ICalendar
    {
        
        private readonly ICrudRepository<CalendarEvent> _repository;
        private readonly ICalendarService<Event> _calendarService;
        private readonly ISynchronizer _synchronizer;
        private readonly IMapper _mapper;
        private readonly IEventBuilder _eventBuilder;
        private readonly ILogger<GoogleCalendar> _logger;

        public GoogleCalendar(
            ICrudRepository<CalendarEvent> repository,
            ICalendarService<Event> calendarService,
            ISynchronizer synchronizer, 
            IMapper mapper, 
            IEventBuilder eventBuilder,
            ILogger<GoogleCalendar> logger)
        {
            

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _calendarService = calendarService ?? throw new ArgumentNullException(nameof(calendarService));
            _synchronizer = synchronizer ?? throw new ArgumentNullException(nameof(synchronizer));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventBuilder = eventBuilder ?? throw new ArgumentNullException(nameof(eventBuilder));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> CreateEventAsync(
            CalendarEventDto eventData, 
            string calendarId = Constants.CalendarId, 
            string timezone = Constants.Timezone, 
            bool isRecurringEvent = false,
            Recurrence period = Recurrence.DAILY,
            int recurringEventsCount = 5,
            DateTime? recurUntilDateTime = null)
        {
            CalendarEvent newDbEvent = _mapper.Map<CalendarEvent>(eventData);

            _eventBuilder.BuildBasicEventWithDescription(
                newDbEvent.Title,
                newDbEvent?.Start,
                newDbEvent?.End,
                newDbEvent.Description);

            if (isRecurringEvent)
            {
                _eventBuilder.SetRecurrence(period, recurringEventsCount, recurUntilDateTime);
            }

           

            return await _repository.InsertAsync(newDbEvent);
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventToBeDeleted = await _repository.GetByIdAsync(id);
            if (eventToBeDeleted != null)
            {
                await _calendarService.DeleteEventAsync(eventToBeDeleted.CalendarId);
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CalendarEventDto>> GetAllEventsAsync(
            int eventsPerPage,
            int pageNumber,
            string calendarId = Constants.CalendarId)
        {
            // Step 1 - synchronize Google calendar with DB
            //await _synchronizer.SynchronizeDatabaseWithCalendarAsync(_googleCrudAccess, _calendarService, _repository);
            await _synchronizer.SynchronizeDatabaseWithCalendarAsync(_calendarService, _repository);

            // Step 2 - get all data from DB
            var eventsResult = await _repository.GetAllAsync(eventsPerPage, pageNumber);

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

            var eventFromDbToUpdate = await _repository.GetByIdAsync(id);
            if (eventFromDbToUpdate != null)
            {
                toUpdateData.CalendarId = eventFromDbToUpdate.CalendarId;
                await _calendarService.UpdateEventAsync(new Event()
                {
                    Summary = toUpdateData.Title
                    // TODO Populate event properties from argument data
                }); 
            }

            return await _repository.UpdateAsync(toUpdateData);
        }
    }
}
