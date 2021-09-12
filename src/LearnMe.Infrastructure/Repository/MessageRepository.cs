
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Messages;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnMe.Infrastructure.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<UserBasic> _userManager;

        public MessageRepository(ApplicationDbContext applicationDbContext, UserManager<UserBasic> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = await _applicationDbContext.Messages.Include(u => u.Sender)
                                                         
                                                         .Include(u => u.Recipient).AsQueryable().ToListAsync();
            
            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    var inboxMessages = messages.Where(u => u.RecipientId == messageParams.id && u.RecipientDeleted == false);
                    return inboxMessages;
                case "Outbox":
                    var outboxMessages = messages.Where(u => u.SenderId == messageParams.id && u.SenderDeleted == false);
                    return outboxMessages;
                default:
                    var defaultMessages = messages.Where(u => u.SenderId == messageParams.id && u.IsRead == false && u.RecipientDeleted == false);
                    return defaultMessages ;
            }           
        }
    }
       
}
