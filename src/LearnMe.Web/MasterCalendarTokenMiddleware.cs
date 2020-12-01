using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
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
            //  TODO check if I can add service IToken to container via middleware
            await _next(context);
        }
    }
}