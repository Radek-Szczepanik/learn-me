using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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

        //public async Task<IToken> InvokeAsync(HttpContext context, out IToken token)
        //{
        //    var token = await _tokenService.GetToken();

        //    // Call the next delegate/middleware in the pipeline
        //    await _next(context);
        //}
    }
}