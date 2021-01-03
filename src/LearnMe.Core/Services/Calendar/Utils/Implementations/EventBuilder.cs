using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils.Constants;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
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

        public void SetStartTime(DateTime? startDateTime, string timezone = CalendarConstants.Timezone)
        {
            this._event.Start = new EventDateTime()
            {
                DateTime = startDateTime,
                TimeZone = timezone
            };
        }

        public void SetEndTime(DateTime? endDateTime, string timezone = CalendarConstants.Timezone)
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
            else if (untilDate != null)
            {
                settings = new string[] { $"RRULE:FREQ={period.ToString()};UNTIL={untilDate}" };
            }
            else
            {
                settings = new string[] { null };
            }

            this._event.Recurrence = settings.ToList();
        }

        public bool AddAttendee(string attendeeEmail)
        {
            if (new EmailAddressAttribute().IsValid(attendeeEmail))
            {
                if (this._event.Attendees == null)
                {
                    this._event.Attendees = new List<EventAttendee>();
                }
                this._event.Attendees.Add(new EventAttendee() { Email = attendeeEmail });
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveAttendee(string attendeeEmail)
        {
            if (new EmailAddressAttribute().IsValid(attendeeEmail))
            {
                this._event.Attendees.Remove(new EventAttendee() { Email = attendeeEmail });
                return true;
            } else
            {
                return false;
            }
        }

        public bool RemoveAllAttendees()
        {
            this._event.Attendees = null;

            return true;
        }

        public bool UpdateAttendees(IList<string> attendeesEmails)
        {
            var currentEmailsList = new List<string>();
            if (this._event.Attendees != null)
            {
                foreach (var person in this._event.Attendees)
                {
                    currentEmailsList.Add(person.Email);
                }
            }

            var emailsToAdd = attendeesEmails.Except(currentEmailsList);
            var emailsToDelete = currentEmailsList.Except(attendeesEmails);

            if (emailsToAdd != null)
            {
                foreach (var email in emailsToAdd)
                {
                    AddAttendee(email);
                }
            }

            if (emailsToDelete != null)
            {
                foreach (var email in emailsToAdd)
                {
                    RemoveAttendee(email);
                }
            }

            return true;
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
