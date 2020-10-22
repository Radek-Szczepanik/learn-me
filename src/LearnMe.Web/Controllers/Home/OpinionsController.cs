using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Home;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnMe.Controllers.Home
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICrudRepository<Opinion> _crudRepository;

        public OpinionsController(ApplicationDbContext context, ICrudRepository<Opinion> crudRepository)
        {
            _context = context;
            _crudRepository = crudRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Opinion>>> GetAllOpinions(int itemsPerPage = 5, int pageNumber = 1)
        {
            return Ok(await _crudRepository.GetAllAsync(itemsPerPage, pageNumber));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Opinion>> GetOpinion(int id)
        {
            var opinion = await _crudRepository.GetByIdAsync(id);

            if (opinion == null)
                return NotFound();

            return Ok(opinion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOpinion(int id, Opinion opinion)
        {
            if (id != opinion.Id && !OpinionExists(id))
                return NotFound();

            await _crudRepository.UpdateAsync(opinion);
            await _crudRepository.SaveAsync();

            return Ok(opinion);
        }

        [HttpPost]
        public async Task<ActionResult<Opinion>> AddOpinion(Opinion opinion)
        {
            await _crudRepository.InsertAsync(opinion);
            await _crudRepository.SaveAsync();

            return Ok(opinion);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Opinion>> DeleteOpinion(int id)
        {
            var opinion = await _crudRepository.GetByIdAsync(id);

            if (opinion == null)
                return NotFound();

            await _crudRepository.DeleteAsync(opinion);
            await _crudRepository.SaveAsync();

            return Ok(opinion);
        }

        private bool OpinionExists(int id)
        {
            return _context.Opinions.Any(o => o.Id == id);
        }
    }
}