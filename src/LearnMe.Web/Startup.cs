using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LearnMe.Infrastructure.Repository;
using LearnMe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Google.Apis.Auth.OAuth2;
using LearnMe.Core.DTOMapper;
using LearnMe.Core.Interfaces.Services;
using LearnMe.Core.Services.Calendar;
using LearnMe.Core.Services.Calendar.Utils.Implementations;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;

namespace LearnMe.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Learn Me API", Version = "v1" });
            });


            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LearnMeDatabase"), b=> b.MigrationsAssembly("LearnMe.Web")));

            services.AddScoped<IGoogleAPIconnection, GoogleAPIconnection>();

            services.AddScoped(async x =>
            {
                var context = x.GetService<IHttpContextAccessor>().HttpContext;

                string[] Scopes = { CustomCalendarService.Scope.Calendar };

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

                // TODO Analyze if this is needed
                context.Items.Add("UserToken", credential);

                return credential ?? throw new ArgumentNullException(nameof(credential));
            });

            services.AddScoped<ICalendar, GoogleCalendar>();
            services.AddScoped<IGoogleCRUD, GoogleCRUD>();
            services.AddScoped<ISynchronizer, Synchronizer>();

            services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
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

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Learn Me API");
            });

            app.UseRouting();

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
