using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class GoogleCRUD : IGoogleCRUD
    {
        public async Task<IEnumerable<Event>> GetAllEventsAsync(
            CalendarService calendarService,
            bool includeCancelled = false, 
            string calendarId = Constants.CalendarId,
            int maxNumberOfResults = 250, // default value form Google API
            string nextPageToken = null) // corresponds to offset zero (< 250 events in calendar)
        {
            EventsResource.ListRequest request = calendarService.Events.List(calendarId);
            request.ShowDeleted = includeCancelled;
            request.MaxResults = maxNumberOfResults;

            if (nextPageToken != null) request.PageToken = nextPageToken;

            Events result = await request.ExecuteAsync();

            if (result.NextPageToken == null)
            {
                return result.Items;
            }
            else
            {
                return await GetAllEventsAsync(
                    calendarService,
                    includeCancelled,
                    calendarId,
                    maxNumberOfResults,
                    result.NextPageToken);
            }
        }
    }
}
