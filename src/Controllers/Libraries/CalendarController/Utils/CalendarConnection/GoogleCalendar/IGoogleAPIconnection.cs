using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;

namespace LearnMe.Controllers.Libraries.CalendarController.Utils.CalendarConnection.GoogleCalendar
{
    public interface IGoogleAPIconnection
    {
        UserCredential GetToken();

        CalendarService CreateCalendarService(UserCredential credential, string applicationName);
    }
}
