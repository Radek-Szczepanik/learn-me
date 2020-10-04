using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class CustomCalendarService : CalendarService, ICalendarService<Event>
    {
        private readonly string _calendarId;

        public CustomCalendarService(IToken credentialToken) 
            : base(new BaseClientService.Initializer() 
            {
                HttpClientInitializer = credentialToken.Credential, 
                ApplicationName = Constants.ApplicationName
            })
        {
            _calendarId = Constants.CalendarId;
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(
            bool includeCancelled = false,
            string calendarId = Constants.CalendarId)
        {
            EventsResource.ListRequest request = base.Events.List(calendarId);
            request.ShowDeleted = includeCancelled;

            Events result = await request.ExecuteAsync();

            return result.Items;
        }

        public async Task<Event> InsertEventAsync(Event obj)
        {
            var createdEvent = await base.Events.Insert(obj, _calendarId).ExecuteAsync();

            return createdEvent;
        }

        public async Task<bool> UpdateEventAsync(Event obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
