using System;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Shared.Enum.Calendar;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface IEventBuilder
    {
        void BuildBasicEvent(
            string summary,
            DateTime? start,
            DateTime? end);

        void BuildBasicEventWithDescription(
            string summary,
            DateTime? start,
            DateTime? end,
            string description);

        void SetRecurrence(
            Recurrence period,
            int numberOfEvents = 0,
            DateTime? untilDate = null);

        bool AddAttendee(string email);

        bool RemoveAttendee(string attendeeEmail);

        Event GetEvent();
    }
}
