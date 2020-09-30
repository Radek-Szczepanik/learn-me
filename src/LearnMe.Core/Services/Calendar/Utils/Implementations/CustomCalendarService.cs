using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Http;
using Microsoft.AspNetCore.Http;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class CustomCalendarService : CalendarService, ICalendarService<Event>
    {
        private readonly string _calendarId;
        private readonly IHttpContextAccessor _accessor;

        public CustomCalendarService(IHttpContextAccessor accessor) : base(new BaseClientService.Initializer()
            {
                HttpClientInitializer = (UserCredential)accessor.HttpContext.Items["UserToken"],
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


        //public CustomCalendarService(
        //    IHttpContextAccessor accessor) : base()
        //{
        //    _accessor = accessor;
        //    _accessor.HttpContext.Items.TryGetValue("UserToken", out var token);

        //    new CalendarService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = (UserCredential)token,
        //        ApplicationName = Constants.ApplicationName,
        //    });

        //    _calendarId = Constants.CalendarId;
        //}

        //public CustomCalendarService(
        //    IHttpContextAccessor accessor,
        //    IConfigurableHttpClientInitializer initializer) : base(new BaseClientService.Initializer()
        //{
        //    HttpClientInitializer = (UserCredential)token,
        //    ApplicationName = Constants.ApplicationName,
        //})
        //{
        //    _accessor = accessor;
        //    _accessor.HttpContext.Items.TryGetValue("UserToken", out var token);

        //    var tokenTry = _accessor.HttpContext.Items.

        //        _calendarId = Constants.CalendarId;
        //}

        //public CustomCalendarService(IHttpContextAccessor accessor)
        //{
        //    _accessor = accessor;
        //    _accessor.HttpContext.Items.TryGetValue("UserToken", out var token);

        //    base(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = (UserCredential)token,
        //        ApplicationName = Constants.ApplicationName,
        //    });

        //    base.HttpClientInitializer = (UserCredential)token;

        //    base.

        //        _calendarId = Constants.CalendarId;
        //}

        //public CustomCalendarService(IHttpContextAccessor accessor) : base(new BaseClientService.Initializer()
        //{
        //    HttpClientInitializer = (UserCredential)accessor.HttpContext.Items["UserToken"],
        //    ApplicationName = Constants.ApplicationName
        //})
        //{
        //    //_accessor = accessor;
        //    //_accessor.HttpContext.Items.TryGetValue("UserToken", out var token);

        //    //HttpClientInitializer = (UserCredential) token;
        //    //override

        //    //base(new BaseClientService.Initializer()
        //    //{
        //    //    HttpClientInitializer = (UserCredential)token,
        //    //    ApplicationName = Constants.ApplicationName,
        //    //});

        //    //base.HttpClientInitializer = (UserCredential) token;

        //    //base.

        //    _calendarId = Constants.CalendarId;
        //}
    }
}
