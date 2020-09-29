//using Google.Apis.Calendar.v3.Data;
//using LearnMe.Core.Services.Calendar.Utils.Interfaces;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Google.Apis.Calendar.v3;

//namespace LearnMe.Core.Services.Calendar.Utils.Implementations
//{
//    public class GoogleCRUD : IGoogleCRUD
//    {
//        public async Task<IEnumerable<Event>> GetAllEventsAsync(
//            CustomCalendarService calendarService, 
//            bool includeCancelled = false, 
//            string calendarId = Constants.CalendarId)
//        {
//            EventsResource.ListRequest request = calendarService.Events.List(calendarId);
//            request.ShowDeleted = includeCancelled;
            
//            Events result = await request.ExecuteAsync();

//            return result.Items;
//        }
//    }
//}
