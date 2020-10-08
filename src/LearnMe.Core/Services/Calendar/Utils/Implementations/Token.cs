using Google.Apis.Auth.OAuth2;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class Token : IToken
    {
        public UserCredential Credential { get; set; }
    }
}
