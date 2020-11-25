using AutoMapper;
using LearnMe.Core.DTO.Messages;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Messages;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnMe.Controllers.Messages
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ICrudRepository<Message> _crudRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MessagesController(ICrudRepository<Message> crudRepository, ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _crudRepository = crudRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            var messages = await _crudRepository.GetAllAsync();
            var messagesToReturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(messages);
            return Ok(messagesToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var mesage = await _crudRepository.GetByIdAsync(id);

            if (mesage == null)
                return NotFound();

            var messageToReturn = _mapper.Map<MessageToReturnDto>(mesage);
            return Ok(messageToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessage(Message message)
        {
            await _crudRepository.InsertAsync(message);
            await _crudRepository.SaveAsync();

            return Ok(message);
        }
    }
}
