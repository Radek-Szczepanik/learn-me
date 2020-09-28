using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface IGoogleAPIconnection
    {
        Task<UserCredential> GetToken();

        CalendarService CreateCalendarService(UserCredential credential, string applicationName);
    }
}
