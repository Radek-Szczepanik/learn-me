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
using LearnMe.Core.DTOMapper;
using LearnMe.Core.Interfaces.Services;
using LearnMe.Core.Services.Calendar;
using LearnMe.Core.Services.Calendar.Utils.Implementations;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Threading;
using Google.Apis.Calendar.v3.Data;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Learn Me API", Version = "v1" });
            });


            services.AddControllersWithViews();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LearnMeDatabase"), b=> b.MigrationsAssembly("LearnMe.Web")));

            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IToken>(provider =>
            {
                var tokenService = provider.GetService<ITokenService>();
                var token = tokenService.GetToken().GetAwaiter().GetResult();
                return token;
            });

            services.AddScoped(typeof(IExternalCalendarService<>), typeof(ExternalCalendarService));
            services.AddScoped<ICalendar, Core.Services.Calendar.Calendar>();
            services.AddScoped<ISynchronizer, Synchronizer>();

            services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));
            services.AddScoped(typeof(ICalendarEventsRepository), typeof(CalendarEventsRepository));

            services.AddSingleton<IEventBuilder, EventBuilder>();

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
