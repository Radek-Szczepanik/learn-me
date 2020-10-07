using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class TokenService : ITokenService
    {
        public async Task<IToken> GetToken()
        {
            string[] Scopes = { ExternalCalendarService.Scope.Calendar };

            using var stream = new FileStream("..\\LearnMe.Core\\Services\\Calendar\\Utils\\Credentials\\credentials.json", FileMode.Open, FileAccess.Read);

            // The file token.json stores the user's access and refresh tokens, and is created
            // automatically when the authorization flow completes for the first time.
            string credPath = "..\\LearnMe.Core\\Services\\Calendar\\Utils\\Credentials\\token.json";

            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "testaspnetgooglapi@gmail.com",
                CancellationToken.None,
                new FileDataStore(credPath, true));

            IToken token = new Token() { Credential = credential };

            return token;
        }
    }
}
