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
    public class NewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICrudRepository<News> _crudRepository;

        public NewsController(ICrudRepository<News> crudRepository, ApplicationDbContext context)
        {
            _context = context;
            _crudRepository = crudRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetAllNews(int itemsPerPage = 5, int pageNumber = 1)
        {
            return Ok(await _crudRepository.GetAllAsync(itemsPerPage, pageNumber));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNews(int id)
        {
            var exercise = await _crudRepository.GetByIdAsync(id);

            if (exercise == null)
                return NotFound();

            return Ok(exercise);
        }

        [HttpPost]
        public async Task<ActionResult<News>> AddNews(News news)
        {
            await _crudRepository.InsertAsync(news);
            await _crudRepository.SaveAsync();

            return Ok(news);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<News>> EditNews(int id, News news)
        {
            if (id != news.Id && !NewsExists(id))
            {
                return BadRequest();
            }

            await _crudRepository.UpdateAsync(news);
            await _crudRepository.SaveAsync();

            return Ok(news);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<News>> DeleteNews(int id)
        {
            var news = await _crudRepository.GetByIdAsync(id);

            if (news == null)
                return NotFound();

            await _crudRepository.DeleteAsync(news);
            await _crudRepository.SaveAsync();

            return Ok(news);
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(n => n.Id == id);
        }
    }
}
