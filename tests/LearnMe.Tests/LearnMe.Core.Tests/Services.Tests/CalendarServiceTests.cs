using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils;
using LearnMe.Core.Services.Calendar.Utils.Implementations;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnMe.Core.Tests.Services.Tests
{
    public class CalendarServiceTests
    {
        private readonly Mock<IGoogleCRUD> _googleCRUDMock = new Mock<IGoogleCRUD>();
        private readonly Mock<CalendarService> _calendarServiceMock = new Mock<CalendarService>();
        private readonly Mock<ICrudRepository<CalendarEvent>> _repositoryMock = new Mock<ICrudRepository<CalendarEvent>>();

        private Synchronizer _synchronizer;

        [SetUp]
        public void Setup()
        {
            _synchronizer = new Synchronizer();
        }

        [Test]
        public async Task SynchronizeDatabaseWithCalendarAsync_Returns_0_WhenNoDifferenceBetweenCalendarAndDatabase()
        {
            // Arrange
            IEnumerable<Event> eventsFromCalendarResult = new List<Event>()
            {
                //_calendarServiceMock.Object.Events.Get(It.IsAny<string>(), It.IsAny<string>()).,

            };
            IList<string> databaseCalendarIds = Helpers.GetListOfCalendarIdsFromDatabase(_repositoryMock.Object); // zero
            // Making sure all of the calendar events Id's are included in database calendar Ids list
            foreach (var calendarEvent in eventsFromCalendarResult)
            {
                databaseCalendarIds.Add(calendarEvent.Id);
                // TODO: The below is probably wrong because uses method from another class to Arrange before test (???)
                await _repositoryMock.Object.InsertAsync(new CalendarEvent()
                {
                    Title = null,
                    Description = null,
                    Start = null,
                    End = null,
                    IsDone = false,
                    CalendarId = calendarEvent.Id
                });
            }

            // Act
            int result = await _synchronizer.SynchronizeDatabaseWithCalendarAsync(
                _googleCRUDMock.Object,
                _calendarServiceMock.Object,
                _repositoryMock.Object,
                "primary");

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}