using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Controllers.Libraries.CalendarController.DTO;
using LearnMe.Controllers.Libraries.CalendarController.Utils.CalendarConnection;
using LearnMe.Controllers.Libraries.CalendarController.Utils.CalendarConnection.GoogleCalendar;
using LearnMe.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LearnMe.Controllers.Libraries.CalendarController.BusinessLogic;

namespace LearnMe.Controllers.Libraries.CalendarController
{
    [Route("api/[controller]")]
    public class CalendarEventsController : Controller
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string ApplicationName = "Learn Me WEB Applicaton";
        private readonly ILogger<CalendarEventsController> _logger;
        private readonly CalendarService _calendarConnection;
        private readonly IExternalCalendarService<Event> _calendarService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CalendarEventsController(
            ILogger<CalendarEventsController> logger,
            IGoogleAPIconnection googleAPIconnection,
            IExternalCalendarService<Event> calendarService,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _logger = logger;
            var token = googleAPIconnection.GetToken();
            _calendarConnection = googleAPIconnection.CreateCalendarService(token, ApplicationName);
            _calendarService = calendarService;
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<CalendarEventDTO>> GetAsync()
        {
            throw new NotImplementedException();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarEventDTO>> GetByIdAsync(int id)
        {
            var foundEvent = await CalendarLogic.FindCalendarEventByIdAsync(_context, id);
            if (foundEvent == null)
            {
                return NotFound();
            }

            return Ok(foundEvent);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //var eventToAdd = JsonSerializer.DeserializeAsync<CalendarEvent>(new StringContent(value, Encoding.UTF8));

            //CalendarEvent toAdd = new CalendarEvent();
            //var result = await _context.SaveChangesAsync(toAdd);


            //var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            //var result = await _categoryService.SaveAsync(category);

            //if (!result.Success)
            //{
            //    return BadRequest(new ErrorResource(result.Message));
            //}

            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Resource);
            //return Ok(categoryResource);

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (await CalendarLogic.DeleteEventAsync(_calendarConnection, _context, id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
