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
            // Arrange

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
            // Arrange

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
            // Arrange

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
            // Arrange

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
            // Arrange

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
            // Arrange

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
        [TestCase(Recurrence.DAILY, Int32.MinValue)]
        [TestCase(Recurrence.DAILY, -1)]
        [TestCase(Recurrence.DAILY, 0)]
        [TestCase(Recurrence.WEEKLY, 1)]
        public void SetRecurrenceByNumberOfEvents_GivenUntilDateIsNull_SetsRecurrenceFieldWithGivenValue(
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

        [TestCase(Recurrence.DAILY, 1)]
        [TestCase(Recurrence.DAILY, 2)]
        [TestCase(Recurrence.DAILY, 10)]
        [TestCase(Recurrence.DAILY, 01)]
        [TestCase(Recurrence.DAILY, Int32.MaxValue)]
        [TestCase(Recurrence.DAILY, Int32.MinValue)]
        [TestCase(Recurrence.DAILY, -1)]
        [TestCase(Recurrence.DAILY, 0)]
        [TestCase(Recurrence.WEEKLY, 1)]
        public void SetRecurrenceByNumberOfEvents_GivenUntilDateIsValid_SetsRecurrenceFieldWithGivenValue(
            Recurrence period,
            int numberOfEvents)
        {
            // Arrange
            DateTime? untilDate = DateTime.Parse("01/20/2012");

            // Act
            _eventBuilderInstanceToTest.SetRecurrence(period, numberOfEvents, untilDate);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual($"RRULE:FREQ={period.ToString()};COUNT={numberOfEvents}",
                result.Recurrence[0]);
        }

        [TestCase(Recurrence.WEEKLY, 1, "01/20/2012")]
        [TestCase(Recurrence.WEEKLY, 2, "01/20/2030")]
        [TestCase(Recurrence.WEEKLY, 10, null)]
        [TestCase(Recurrence.WEEKLY, 01, null)]
        [TestCase(Recurrence.WEEKLY, Int32.MaxValue, "01/20/2012")]
        [TestCase(Recurrence.WEEKLY, Int32.MinValue, "01/20/2012")]
        [TestCase(Recurrence.WEEKLY, -1, "01/20/2012")]
        [TestCase(Recurrence.WEEKLY, 0, "01/20/2012")]
        [TestCase(Recurrence.DAILY, 1, "01/20/2012")]
        public void SetRecurrenceUntilDate_SetsRecurrenceFieldWithGivenValue(
            Recurrence period,
            int numberOfEvents,
            DateTime? untilDate)
        {
            // Act
            _eventBuilderInstanceToTest.SetRecurrence(period, 0, untilDate);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual($"RRULE:FREQ={period.ToString()};UNTIL={untilDate}",
                result.End.DateTime);
        }
    }
}