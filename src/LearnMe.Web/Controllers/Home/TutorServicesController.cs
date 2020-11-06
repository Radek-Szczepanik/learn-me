using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Home;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnMe.Controllers.Home
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICrudRepository<TutorService> _crudRepository;

        public TutorServicesController(ApplicationDbContext context, ICrudRepository<TutorService> crudRepository)
        {
            _context = context;
            _crudRepository = crudRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutorService>>> GetAllTutorServices(int itemsPerPage = 5, int pageNumber = 1)
        {
            return Ok(await _crudRepository.GetAllWithPagination(itemsPerPage, pageNumber));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TutorService>> GetTutorService(int id)
        {
            var tutorService = await _crudRepository.GetByIdAsync(id);

            if (tutorService == null)
                return NotFound();

            return Ok(tutorService);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTutorService(int id, TutorService tutorService)
        {
            if (id != tutorService.Id && !TutorServiceExists(id))
                return NotFound();

            await _crudRepository.UpdateAsync(tutorService);
            await _crudRepository.SaveAsync();

            return Ok(tutorService);
        }

        [HttpPost]
        public async Task<ActionResult<TutorService>> AddTutorService(TutorService tutorService)
        {
            await _crudRepository.InsertAsync(tutorService);
            await _crudRepository.SaveAsync();

            return Ok(tutorService);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TutorService>> DeleteTutorService(int id)
        {
            var tutorService = await _crudRepository.GetByIdAsync(id);

            if (tutorService == null)
                return NotFound();

            await _crudRepository.DeleteAsync(tutorService);
            await _crudRepository.SaveAsync();

            return Ok(tutorService);
        }

        private bool TutorServiceExists(int id)
        {
            return _context.TutorServices.Any(t => t.Id == id);
        }
    }
}