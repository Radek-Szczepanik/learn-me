using System.Threading.Tasks;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface ITokenService
    {
        Task<IToken> GetTokenAsync();
    }
}
