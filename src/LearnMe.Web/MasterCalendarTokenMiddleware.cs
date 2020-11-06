using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Interfaces.Services;
using LearnMe.Core.Services.Calendar.Utils.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace LearnMe.Web
{
    public class MasterCalendarTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public MasterCalendarTokenMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context, IServiceCollection services) //, out IToken token)
        {
            //  TODO check if I can add service IToken to contener via middleware
            //    var token = await _tokenService.GetToken();
            // Console.WriteLine("Custom middleware invoked");
            // Call the next delegate/middleware in the pipeline

            var token = await _tokenService.GetToken();
            services.AddSingleton<IToken>(token);
            //services.AddSingleton<IToken>(token);
            //services.AddScoped<IExternalCalendarService<Event>, ExternalCalendarService>();
            //services.AddScoped<ICalendar, LearnMe.Core.Services.Calendar.Calendar>();

            //services.AddSingleton<IToken>(provider =>
            //{
            //    var tokenService = provider.GetService<ITokenService>();
            //    var token = tokenService.GetToken().GetAwaiter().GetResult();
            //    return token;
            //});

            await _next(context);
        }
    }
}