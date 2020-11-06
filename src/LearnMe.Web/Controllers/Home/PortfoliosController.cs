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
    public class PortfoliosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICrudRepository<Portfolio> _crudRepository;

        public PortfoliosController(ApplicationDbContext context, ICrudRepository<Portfolio> crudRepository)
        {
            _context = context;
            _crudRepository = crudRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetAllPortfolios(int itemsPerPage = 5, int pageNumber = 1)
        {
            return Ok(await _crudRepository.GetAllWithPagination(itemsPerPage, pageNumber));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolio(int id)
        {
            var portfolio = await _crudRepository.GetByIdAsync(id);

            if (portfolio == null)
                return NotFound();

            return Ok(portfolio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPortfolio(int id, Portfolio portfolio)
        {
            if (id != portfolio.Id && !PortfolioExists(id))
                return NotFound();

            await _crudRepository.UpdateAsync(portfolio);
            await _crudRepository.SaveAsync();

            return Ok(portfolio);
        }

        [HttpPost]
        public async Task<ActionResult<Portfolio>> AddPortfolio(Portfolio portfolio)
        {
            await _crudRepository.InsertAsync(portfolio);
            await _crudRepository.SaveAsync();

            return Ok(portfolio);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Portfolio>> DeletePortfolio(int id)
        {
            var portfolio = await _crudRepository.GetByIdAsync(id);

            if (portfolio == null)
                return NotFound();

            await _crudRepository.DeleteAsync(portfolio);
            await _crudRepository.SaveAsync();

            return Ok(portfolio);
        }

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(p => p.Id == id);
        }
    }
}
