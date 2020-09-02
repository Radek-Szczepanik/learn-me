using LearnMeAPI.Models;
using System.Threading.Tasks;

namespace LearnMeAPI.Data
{
    public interface IAuthRepository
    {
        Task<User> Login(string username, string password);
        Task<User> Register(User user, string password);
        Task<bool> UserExist(string username);
    }
}
