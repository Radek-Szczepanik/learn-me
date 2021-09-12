using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesForUser(MessageParams messageParams);
    }
}
