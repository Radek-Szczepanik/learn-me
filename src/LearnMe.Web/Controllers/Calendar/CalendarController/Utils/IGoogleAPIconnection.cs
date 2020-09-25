using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;

namespace LearnMe.Web.Controllers.Libraries.CalendarController.Utils
{
    public interface IGoogleAPIconnection
    {
        UserCredential GetToken();

        CalendarService CreateCalendarService(UserCredential credential, string applicationName);
    }
}
