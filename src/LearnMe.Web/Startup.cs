using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.DTO.Config;
using LearnMe.Core.Interfaces.Services;
using LearnMe.Core.Services.Account;
using LearnMe.Core.Services.Account.Email;
using LearnMe.Core.Services.Calendar.Utils.Implementations;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NETCore.MailKit.Core;


namespace LearnMe.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Learn Me API", Version = "v1" });
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.AddDbContext<ApplicationDbContext>(
             options => options.UseSqlServer(Configuration.GetConnectionString("LearnMeDatabase"),
             b => b.MigrationsAssembly("LearnMe.Web")));
            services.AddIdentity<UserBasic, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddAuthentication()
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                Configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "YourAppCookieName";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/";

                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            services.AddScoped<IUserClaimsPrincipalFactory<UserBasic>, MyUserClaimsPrincipalFactory>();

            services.AddSingleton<ITokenService, TokenService>();

            services.AddSingleton<IToken>(provider =>
            {
                var tokenService = provider.GetService<ITokenService>();
                var token = tokenService.GetToken().GetAwaiter().GetResult();
                return token;
            });

            services.AddCors();
            services.AddScoped<IExternalCalendarService<Event>, ExternalCalendarService>();
            services.AddScoped<ICalendar, LearnMe.Core.Services.Calendar.Calendar>();
            services.AddScoped<ISynchronizer, Synchronizer>();

            services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));
            services.AddScoped(typeof(ICalendarEventsRepository), typeof(CalendarEventsRepository));

            services.AddSingleton<IEventBuilder, EventBuilder>();

            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfiles()); });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("/wwwwroot")
            });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Learn Me API"); });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Custom middleware
            //app.UseMasterCalendarToken();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
