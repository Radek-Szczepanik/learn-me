using Microsoft.AspNetCore.Builder;

namespace LearnMe.Web
{
    public static class MasterCalendarTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseMasterCalendarToken(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MasterCalendarTokenMiddleware>();
        }
    }
}
