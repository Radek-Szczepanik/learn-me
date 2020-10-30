using System;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Interfaces.Services;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Calendar = LearnMe.Core.Services.Calendar.Calendar;

namespace LearnMe.Core.Tests.ServicesTests.CalendarTests
{
    public class CalendarTests
    {
        private ICrudRepository<CalendarEvent> _repository;
        private ICalendarEventsRepository _calendarEventsRepository;
        private IExternalCalendarService<Event> _externalCalendarService;
        private ISynchronizer _synchronizer;
        private IMapper _mapper;
        private IEventBuilder _eventBuilder;
        private ILogger<Calendar> _logger;

        private ICalendar _calendarInstanceToTest;

        [SetUp]
        public void Setup()
        {
            var repositoryMock = new Mock<ICrudRepository<CalendarEvent>>();
            var calendarEventsRepositoryMock = new Mock<ICalendarEventsRepository>();
            var externalCalendarServiceMock = new Mock<IExternalCalendarService<Event>>();
            var synchronizerMock = new Mock<ISynchronizer>();
            var mapperMock = new Mock<IMapper>();
            var eventBuilderMock = new Mock<IEventBuilder>();
            var loggerMock = new Mock<ILogger<Calendar>>();

            // mock config
            mapperMock.Setup(x => x.Map<CalendarEvent>(It.IsAny<CalendarEventDto>()))
                .Returns(new CalendarEvent());
            mapperMock.Setup(x => x.Map<CalendarEventDto>(It.IsAny<CalendarEvent>()))
                .Returns(new CalendarEventDto());
            eventBuilderMock.Setup(x => x.GetEvent())
                .Returns(new Event());
            externalCalendarServiceMock.Setup(x => x.InsertEventAsync(It.IsAny<Event>()))
                .ReturnsAsync(new Event());
            repositoryMock.Setup(x => x.InsertAsync(It.IsAny<CalendarEvent>()))
                .ReturnsAsync(new CalendarEvent());

            _repository = repositoryMock.Object;
            _calendarEventsRepository = calendarEventsRepositoryMock.Object;
            _externalCalendarService = externalCalendarServiceMock.Object;
            _synchronizer = synchronizerMock.Object;
            _mapper = mapperMock.Object;
            _eventBuilder = eventBuilderMock.Object;
            _logger = loggerMock.Object;

            _calendarInstanceToTest = new Calendar(
                _repository,
                _calendarEventsRepository,
                _externalCalendarService,
                _synchronizer,
                _mapper,
                _eventBuilder,
                _logger);
        }

        [TestCase("Subject",
            "Description",
            "01/20/2012", // nUnit automatically does DateTime.Parse()
            "02/20/2012",
            false,
            false,
            "primary")]
        public async Task CreateEventAsync_CreateValidNotRecurringEvent_ReturnsCreatedEventDto(
            string subject,
            string description,
            DateTime startDate,
            DateTime endDate,
            bool isDone,
            bool isFreeSlot,
            string calendarId)
        {
            // Arrange
            var eventToAdd = new CalendarEventDto()
            {
                Subject = subject,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                IsDone = isDone,
                IsFreeSlot = isFreeSlot,
                CalendarId = calendarId
            };

            // Act
            var result = await _calendarInstanceToTest.CreateEventAsync(eventToAdd);

            // Assert
            Assert.IsInstanceOf<CalendarEventDto>(result);
        }
    }
}