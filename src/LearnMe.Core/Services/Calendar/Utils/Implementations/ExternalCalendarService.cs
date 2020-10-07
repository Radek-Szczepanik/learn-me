using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class ExternalCalendarService : CalendarService, IExternalCalendarService<Event>
    {
        private readonly string _calendarId;

        public ExternalCalendarService(IToken credentialToken)
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
            string result = await base.Events.Delete(_calendarId, id).ExecuteAsync();

            return result != null;
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            {
                Event result = await base.Events.Get(_calendarId, id).ExecuteAsync();

                return result;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(bool includeCancelled = false)
        {
            EventsResource.ListRequest request = base.Events.List(_calendarId);
            request.ShowDeleted = includeCancelled;

            Events result = await request.ExecuteAsync();

            return result.Items;
        }

        public async Task<Event> InsertEventAsync(Event obj)
        {
            var createdEvent = await base.Events.Insert(obj, _calendarId).ExecuteAsync();

            return createdEvent;
        }

        public async Task<bool> UpdateEventAsync(string id, Event obj)
        {
            var updatedEvent = await base.Events.Update(obj, _calendarId, id).ExecuteAsync();

            return updatedEvent != null;
        }
    }
}
