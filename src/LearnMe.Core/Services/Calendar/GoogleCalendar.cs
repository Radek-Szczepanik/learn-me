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
using Microsoft.Extensions.Logging;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Core.Services.Calendar
{
    public class GoogleCalendar : ICalendar
    {
        private readonly CalendarService _calendarService;
        private readonly string _applicationName = "Learn Me WEB Applicaton";
        private readonly ICrudRepository<CalendarEvent> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GoogleCalendar> _logger;

        public GoogleCalendar(IGoogleAPIconnection googleAPIconnection,
                              ICrudRepository<CalendarEvent> repository,
                              IMapper mapper,
                              ILogger<GoogleCalendar> logger)
        {
            var token = googleAPIconnection.GetToken();
            _calendarService = googleAPIconnection.CreateCalendarService(token, _applicationName);
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<bool> CreateEventAsync(CalendarEventDto eventData)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteEventAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        private void SynchronizeDatabaseWithCalendar(string calendarId = "primary")
        {
            EventsResource.ListRequest request = _calendarService.Events.List(calendarId);
            IEnumerable<Event> eventsFromCalendarResult = request.ExecuteAsync().Result.Items;

            var allDatabaseEvents = _repository.GetAllAsync().Result;
            IList<string> databaseEventsCalendarIds = new List<string>();
            foreach (var eventFromDatabase in allDatabaseEvents)
            {
                databaseEventsCalendarIds.Add(eventFromDatabase.CalendarId);
            }

            foreach (var eventResult in eventsFromCalendarResult)
            {
                // TODO: Add to Calendar specific log
                //Console.WriteLine($"{eventResult.Summary}\n {eventResult.Location}\n {eventResult.Start.DateTime}");

                if (!databaseEventsCalendarIds.Contains(eventResult.Id))
                {
                    _repository.InsertAsync(new CalendarEvent()
                    {
                        Title = eventResult.Summary,
                        Description = eventResult.Description,
                        Start = eventResult.Start.DateTime,
                        End = eventResult.End.DateTime,
                        IsDone = false,
                        CalendarId = eventResult.Id
                    });
                }
            }
        }

        public async Task<IEnumerable<CalendarEventDto>> GetAllEventsAsync(string calendarId = "primary")
        {
            // Step 1 - synchronize Google calendar with DB
            //SynchronizeDatabaseWithCalendar();

            // Step 2 - get all data from DB
            var eventsResult = await _repository.GetAllAsync();

            IList<CalendarEventDto> results = new List<CalendarEventDto>();
            foreach (var eventResult in eventsResult)
            {
                results.Add(_mapper.Map<CalendarEventDto>(eventResult));
            }

            return results;
        }

        public Task<CalendarEventDto> GetEventByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateEventAsync(int id, CalendarEventDto eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}
