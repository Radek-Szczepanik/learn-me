using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;


namespace LearnMe.Core.Services.Calendar.Utils
{
    public class GoogleAPIconnection : IGoogleAPIconnection
    {
        public CalendarService CreateCalendarService(UserCredential cred, string appName)
        {
            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred,
                ApplicationName = appName,
            });

            return service;
        }

        public UserCredential GetToken()
        {
            string[] Scopes = { CalendarService.Scope.Calendar };

            UserCredential credential;

            using var stream = new FileStream("..\\LearnMe.Core\\Services\\Calendar\\Utils\\Credentials\\credentials.json", FileMode.Open, FileAccess.Read);
            // The file token.json stores the user's access and refresh tokens, and is created
            // automatically when the authorization flow completes for the first time.
            string credPath = "..\\LearnMe.Core\\Services\\Calendar\\Utils\\Credentials\\token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "testaspnetgooglapi@gmail.com",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            Console.WriteLine("Credential file saved to: " + credPath);

            return credential;
        }
    }
}

