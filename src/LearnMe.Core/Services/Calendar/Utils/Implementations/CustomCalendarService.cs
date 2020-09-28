using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class CustomCalendarService : CalendarService, ICalendarService
    {
        public CustomCalendarService(UserCredential credentials) : base()
        {
            new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = Constants.ApplicationName,
            });
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
