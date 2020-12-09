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
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICrudRepository<Question> _crudRepository;

        public QuestionsController(ApplicationDbContext context, ICrudRepository<Question> crudRepository)
        {
            _context = context;
            _crudRepository = crudRepository;
        }

          
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetAllQuestions()
        {
            return Ok(await _crudRepository.GetAllAsync());
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Question>>> GetAllQuestions(int itemsPerPage = 5, int pageNumber = 1)
        // {
        //     return Ok(await _crudRepository.GetAllWithPagination(itemsPerPage, pageNumber));
        // }



        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _crudRepository.GetByIdAsync(id);

            if (question == null)
                return NotFound();

            return Ok(question);
        }

        [HttpPut]
        public async Task<IActionResult> EditQuestion(Question question)
        {
            await _crudRepository.UpdateAsync(question);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Question>> AddQuestion(Question question)
        {
            await _crudRepository.InsertAsync(question);
            // await _crudRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Question>> DeleteQuestion(int id)
        {
            var question = await _crudRepository.GetByIdAsync(id);

            if (question == null)
                return NotFound();

            await _crudRepository.DeleteAsync(question);
            await _crudRepository.SaveAsync();

            return Ok(question);
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(q => q.Id == id);
        }
    }
}