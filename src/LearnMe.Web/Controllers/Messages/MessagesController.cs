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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace LearnMe.Controllers.Messages
{
    [Route("api/[controller]/")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ICrudRepository<Message> _crudRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<UserBasic> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMessageRepository _messageRepository;


        public MessagesController(ICrudRepository<Message> crudRepository,
                                  IMapper mapper,
                                  UserManager<UserBasic> userManager,
                                  ApplicationDbContext applicationDbContext,
                                  IMessageRepository messageRepository)

        {
            _crudRepository = crudRepository;
            _mapper = mapper;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _messageRepository = messageRepository;
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
        public async Task<ActionResult> GetMessagesForUser(string email, [FromQuery] MessageParams messageParams)
        {
            var user = await _userManager.FindByEmailAsync(email);
            messageParams.id = user.Id;
            var messagesFromRepo = await _messageRepository.GetMessagesForUser(messageParams);
            var messagesToReturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            foreach (var message in messagesToReturn)
            {
                message.MessageContainer = messageParams.MessageContainer;
            }

            return Ok(messagesToReturn);
        }


        [HttpPost(Name = "GetMessage")]
        public async Task<ActionResult> CreateMessage(MessageToCreateDto messageToCreate)
        {
            //if (email != messageToCreate.SenderId)
            //    return Unauthorized();

            var sender = await _userManager.FindByEmailAsync(messageToCreate.SenderEmail);

            messageToCreate.SenderId = sender.Id;

            var recipient = await _userManager.FindByEmailAsync(messageToCreate.RecipientEmail);

            messageToCreate.RecipientId = recipient.Id;

            if (recipient == null)
                return BadRequest("Nie można znaleźć użytkownika");

            var message = _mapper.Map<Message>(messageToCreate);

            await _crudRepository.InsertAsync(message);

            return Ok();

        }

        [HttpDelete]
         public async Task<ActionResult> DeleteMessage(int id)
        {
            var messageFromRepo = await _crudRepository.GetByIdAsync(id);
            await _crudRepository.DeleteAsync(messageFromRepo);
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> DeleteMessageById(int id, string userId)
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
