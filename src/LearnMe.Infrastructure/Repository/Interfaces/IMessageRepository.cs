using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Messages;
using System.Threading.Tasks;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface IMessageRepository
    {
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
    }
}
