using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using LearnMe.Infrastructure.Models.Domains.Shop;
using LearnMe.Shared.Enum.Calendar;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class EventBuilder : IEventBuilder
    {
        private Event _event = new Event();

        public EventBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._event = new Event();
        }

        public void SetSummary(string summary)
        {
            this._event.Summary = summary;
        }

        public void SetDescription(string description)
        {
            this._event.Description = description;
        }

        public void SetStartTime(DateTime? startDateTime, string timezone = Constants.Timezone)
        {
            this._event.Start = new EventDateTime()
            {
                DateTime = startDateTime,
                TimeZone = timezone
            };
        }

        public void SetEndTime(DateTime? endDateTime, string timezone = Constants.Timezone)
        {
            this._event.End = new EventDateTime()
            {
                DateTime = endDateTime,
                TimeZone = timezone
            };
        }

        public void SetRecurrence(
            Recurrence period,
            int numberOfEvents = 0,
            DateTime? untilDate = null)
        {
            string[] settings;

            if (numberOfEvents > 0)
            {
                settings = new string[] { $"RRULE:FREQ={period.ToString()};COUNT={numberOfEvents}" };
            }
            else
            {
                settings = new string[] { $"RRULE:FREQ={period.ToString()};UNTIL={untilDate}" };
            }

            this._event.Recurrence = settings.ToList();
        }

        public bool AddAttendee(string attendeeEmail)
        {
            if (new EmailAddressAttribute().IsValid(attendeeEmail))
            {
                this._event.Attendees.Add(new EventAttendee() { Email = attendeeEmail });
                return true;
            }
            else
            {
                return false;
            }
        }

        public void BuildBasicEvent(
            string summary,
            DateTime? start,
            DateTime? end)
        {
            this.SetSummary(summary);
            this.SetStartTime(start);
            this.SetEndTime(end);
        }

        public void BuildBasicEventWithDescription(
            string summary,
            DateTime? start,
            DateTime? end,
            string description)
        {
            this.BuildBasicEvent(summary, start, end);
           
            this.SetDescription(description);
        }

        public Event GetEvent()
        {
            Event result = this._event;

            this.Reset();

            return result;
        }
    }
}
