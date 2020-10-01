using Google.Apis.Auth.OAuth2;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface IToken
    {
        UserCredential Credential { get; set; }
    }

    public class Token : IToken
    {
        public UserCredential Credential { get; set; }
    }
}
