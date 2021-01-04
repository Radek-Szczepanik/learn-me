using AutoMapper;
using LearnMe.Core.DTO.Config;
using LearnMe.Core.DTO.Messages;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Messages;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnMe.Controllers.Messages
{
    [Route("api/UserBasics/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ICrudRepository<Message> _crudRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<UserBasic> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;


        public MessagesController(ICrudRepository<Message> crudRepository,
                                  IMapper mapper,
                                  UserManager<UserBasic> userManager,
                                  ApplicationDbContext applicationDbContext)

        {
            _crudRepository = crudRepository;
            _mapper = mapper;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMessage(int id)
        {
            var message = await _crudRepository.GetByIdAsync(id);

            if (message == null)
                return NotFound();

            return Ok(message);
        }

        [HttpGet]
        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _applicationDbContext.Messages.Include(u => u.Sender)
                                                         .Include(u => u.Recipient).AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messageParams.UserId && u.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.IsRead == false && u.RecipientDeleted == false);
                    break;
            }

            messages = messages.OrderByDescending(d => d.DateSent);

            return await PagedList<Message>.CreateListAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        // [HttpGet]
        // public async Task<ActionResult> GetMessagesForUser(string userId, [FromQuery] MessageParams messageParams)
        // {
        //     messageParams.UserId = userId;
        //     var messagesFromRepo = await GetMessagesForUser(messageParams);
        //     var messagesToReturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);



        //     return Ok(messagesToReturn);
        // }

        [HttpPost(Name = "GetMessage")]
        public async Task<ActionResult> CreateMessage(string userId, MessageToCreateDto messageToCreate)
        {
            if (userId != messageToCreate.SenderId)
                return Unauthorized();

            messageToCreate.SenderId = userId;

            var recipient = await _userManager.FindByIdAsync(messageToCreate.RecipientId);

            if (recipient == null)
                return BadRequest("Nie można znaleźć użytkownika");

            var message = _mapper.Map<Message>(messageToCreate);

            await _crudRepository.InsertAsync(message);

            return Ok("wiadomość utworzona poprawnie");

        }

        [HttpPost("{id}")]
        public async Task<ActionResult> DeleteMessage(int id, string userId)
        {
            var messageFromRepo = await _crudRepository.GetByIdAsync(id);

            // nadawca usuwa wiadomość u siebie, ale odbiorca ją widzi
            if (messageFromRepo.SenderId == userId)
                messageFromRepo.SenderDeleted = true;

            // odbiorca usuwa wiadomość u siebie
            if (messageFromRepo.RecipientId == userId)
                messageFromRepo.RecipientDeleted = true;

            // wiadomość jest usuwana całkowicie jeżeli nadawca i odbiorca ją usunęli u siebie
            if (messageFromRepo.SenderDeleted && messageFromRepo.RecipientDeleted)
                await _crudRepository.DeleteAsync(messageFromRepo);

            if (await _crudRepository.SaveAsync())
                return NoContent();

            throw new Exception("Błąd podczas usuwania wiadomości");
        }
    }
}
