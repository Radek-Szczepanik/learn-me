using LearnMe.Core.Services.Calendar.Utils.Implementations;
using NUnit.Framework;

namespace LearnMe.Core.Tests.ServicesTests.CalendarTests
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
        [TestCase(null)]
        public void SetSummary_SetsEventSummaryField(string summary)
        {
            // Arrange

            // Act
            _eventBuilderInstanceToTest.SetSummary(summary);
            var result = _eventBuilderInstanceToTest.GetEvent();

            // Assert
            Assert.AreEqual(summary, result.Summary);
        }
    }
}