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
        public async Task<ActionResult<IEnumerable<News>>> GetAllNews()
        {
            return Ok(await _crudRepository.GetAllAsync());
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

        [HttpPut]
        public async Task<ActionResult<News>> EditNews(News news)
        {
           
            await _crudRepository.UpdateAsync(news);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<News>> DeleteNews(int id)
        {
            if (await _crudRepository.DeleteAsync(id)){
               
                return Ok();
            }
            
            return NotFound();

          
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(n => n.Id == id);
        }
    }
}
