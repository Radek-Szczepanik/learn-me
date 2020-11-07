using System;
using LearnMe.Core.Services.Calendar.Utils;
using LearnMe.Core.Services.Calendar.Utils.Implementations;
using LearnMe.Shared.Enum.Calendar;
using NUnit.Framework;

namespace LearnMe.Core.Tests.ServicesTests.CalendarTests.UtilsTests
{
    public class EventBuilderTests
    {
        private EventBuilder _eventBuilderInstanceToTest;

        [SetUp]
        public void Setup()
        {
            _eventBuilderInstanceToTest = new EventBuilder();
        }

        [TestCase("some summary")]
        [TestCase("")]
        [TestCase(" !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~")]
        [TestCase(null)]
        public void SetSummary_SetsEventSummaryFieldWithGivenValue(string summary)
        {
            // Act
            _eventBuilderInstanceToTest.SetSummary(summary);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual(summary, result.Summary);
        }

        [TestCase("some description")]
        [TestCase("")]
        [TestCase(" !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~")]
        [TestCase(null)]
        public void SetDescription_SetsEventDescriptionFieldWithGivenValue(string description)
        {
            // Act
            _eventBuilderInstanceToTest.SetDescription(description);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual(description, result.Description);
        }

        [TestCase("01/20/2012", Constants.Timezone)] // nUnit automatically does DateTime.Parse()
        [TestCase(null, Constants.Timezone)]
        public void SetStartTime_SetsEventStartTimeFieldWithGivenValue(DateTime? startDateTime, string timezone)
        {
            // Act
            _eventBuilderInstanceToTest.SetStartTime(startDateTime, timezone);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual(startDateTime, result.Start.DateTime);
        }

        [TestCase("01/20/2012", Constants.Timezone)] // nUnit automatically does DateTime.Parse()
        [TestCase("01/20/2012", "America/Mexico_City")]
        [TestCase("01/20/2012", "America/Port-au-Prince")]
        [TestCase("01/20/2012", "Etc/GMT-1")]
        [TestCase("01/20/2012", "Etc/UTC")]
        [TestCase("01/20/2012", "US/Eastern")]
        public void SetStartTime_SetsEventTimeZoneFieldWithGivenValue(DateTime? startDateTime, string timezone)
        {
            // Act
            _eventBuilderInstanceToTest.SetStartTime(startDateTime, timezone);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual(timezone, result.Start.TimeZone);
        }

        [TestCase("01/20/2012", Constants.Timezone)] // nUnit automatically does DateTime.Parse()
        [TestCase(null, Constants.Timezone)]
        public void SetEndTime_SetsEventEndTimeFieldWithGivenValue(DateTime? endDateTime, string timezone)
        {
            // Act
            _eventBuilderInstanceToTest.SetEndTime(endDateTime, timezone);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual(endDateTime, result.End.DateTime);
        }

        [TestCase("01/20/2012", Constants.Timezone)] // nUnit automatically does DateTime.Parse()
        [TestCase("01/20/2012", "America/Mexico_City")]
        [TestCase("01/20/2012", "America/Port-au-Prince")]
        [TestCase("01/20/2012", "Etc/GMT-1")]
        [TestCase("01/20/2012", "Etc/UTC")]
        [TestCase("01/20/2012", "US/Eastern")]
        public void SetEndTime_SetsEventTimeZoneFieldWithGivenValue(DateTime? endDateTime, string timezone)
        {
            // Act
            _eventBuilderInstanceToTest.SetEndTime(endDateTime, timezone);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual(timezone, result.End.TimeZone);
        }

        [TestCase(Recurrence.DAILY, 1)]
        [TestCase(Recurrence.DAILY, 2)]
        [TestCase(Recurrence.DAILY, 10)]
        [TestCase(Recurrence.DAILY, 01)]
        [TestCase(Recurrence.DAILY, Int32.MaxValue)]
        [TestCase(Recurrence.WEEKLY, 1)]
        public void SetRecurrenceByNumberOfEvents_GivenNumberOfEventsMoreThanZeroAndUntilDateIsNull_SetsRecurrenceFieldByNumberOfEvents(
            Recurrence period,
            int numberOfEvents)
        {
            // Act
            _eventBuilderInstanceToTest.SetRecurrence(period, numberOfEvents);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual($"RRULE:FREQ={period.ToString()};COUNT={numberOfEvents}",
                result.Recurrence[0]);
        }

        [TestCase(Recurrence.DAILY, Int32.MinValue)]
        [TestCase(Recurrence.DAILY, -1)]
        [TestCase(Recurrence.DAILY, 0)]
        public void SetRecurrence_GivenUntilDateIsValid_SetsRecurrenceByUntilDate(
            Recurrence period,
            int numberOfEvents)
        {
            // Arrange
            DateTime? untilDate = DateTime.UtcNow;

            // Act
            _eventBuilderInstanceToTest.SetRecurrence(period, numberOfEvents, untilDate);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual($"RRULE:FREQ={period.ToString()};UNTIL={untilDate}",
                result.Recurrence[0]);
        }

        [TestCase(Recurrence.DAILY, Int32.MinValue, null)]
        [TestCase(Recurrence.DAILY, -1, null)]
        [TestCase(Recurrence.DAILY, 0, null)]
        public void SetRecurrence_GivenInvalidInput_SetsRecurrenceAsNull(
            Recurrence period,
            int numberOfEvents,
            DateTime? untilDate)
        {
            // Act
            _eventBuilderInstanceToTest.SetRecurrence(period, 0, untilDate);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual(null,
                result.Recurrence[0]);
        }
    }
}