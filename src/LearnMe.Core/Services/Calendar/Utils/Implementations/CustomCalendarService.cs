using Google.Apis.Auth.OAuth2;
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

        public CustomCalendarService(UserCredential credentials) : base()
        {
            new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = Constants.ApplicationName,
            });

            _calendarId = Constants.CalendarId;
        }

        public Task<bool> DeleteEventAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Event> GetEventByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetEventsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Event> InsertEventAsync(Event obj)
        {
            var createdEvent = await base.Events.Insert(obj, _calendarId).ExecuteAsync();

            return createdEvent;
        }

        public Task<bool> UpdateEventAsync(Event obj)
        {
            throw new System.NotImplementedException();
        }


        //public CustomCalendarService(BaseClientService.Initializer baseInitializer, UserCredential credentials)
        //    : base(baseInitializer)
        //{
        //    HttpClientInitializer = credentials,
        //    ApplicationName = Constants.ApplicationName,
        //})
        //{
        //    base.HttpClientInitializer = credentials;
        //    ApplicationName = Constants.ApplicationName;

        //    var service = new CalendarService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credentials,
        //        ApplicationName = Constants.ApplicationName,
        //    });
        //}
    }
}
