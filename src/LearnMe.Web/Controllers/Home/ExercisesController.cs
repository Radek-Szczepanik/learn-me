using AutoMapper;
using LearnMe.Core.DTO.HomeDTO;
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
    public class ExercisesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICrudRepository<Exercises> _crudRepository;
        private readonly IMapper _mapper;
                
        public ExercisesController(ICrudRepository<Exercises> crudRepository, ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _crudRepository = crudRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercises>>> GetAllExercises(int itemsPerPage = 5, int pageNumber = 1)
        {
            var exercises = await _crudRepository.GetAllAsync(itemsPerPage, pageNumber);
            var exercisesToReturn = _mapper.Map<IEnumerable<ExercisesDTO>>(exercises);

            return Ok(exercisesToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exercises>> GetExercise(int id)
        {
            var exercise = await _crudRepository.GetByIdAsync(id);

            if (exercise == null)
                return NotFound();

            return Ok(exercise);
        }

        [HttpPost]
        public async Task<ActionResult<Exercises>> AddExercise(Exercises exercise)
        {
            await _crudRepository.InsertAsync(exercise);
            await _crudRepository.SaveAsync();

            return Ok(exercise);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Exercises>> EditExercise(int id, Exercises exercise)
        {
            if (id != exercise.Id && !ExerciseExists(id))
            {
                return BadRequest();
            }

            await _crudRepository.UpdateAsync(exercise);
            await _crudRepository.SaveAsync();

            return Ok(exercise);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Exercises>> DeleteExercise(int id)
        {
            var exercise = await _crudRepository.GetByIdAsync(id);

            if (exercise == null)
                return NotFound();

            await _crudRepository.DeleteAsync(exercise);
            await _crudRepository.SaveAsync();

            return Ok(exercise);
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }   
}
