using System;
using AutoMapper;
using ElevatorWorkerService.Models;
using ElevatorWorkerService.Models.Blogs;
using ElevatorWorkerService.Services;
using ElevatorWorkerService.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;

namespace ElevatorWorkerService
{
    public class Startup
    {
        // ReSharper disable once MethodTooLong
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => { builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader(); });
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddDistributedMemoryCache();
            services.AddSession(options => { options.Cookie.IsEssential = true; });

            BsonClassMap.RegisterClassMap<BlogUpdateModel>(cm => { cm.AutoMap(); });
            BsonClassMap.RegisterClassMap<Blog>(cm => { cm.AutoMap(); });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IBlogService, BlogService>();

            //ConfigureElevatorServices(services);
        }

        // ReSharper disable once UnusedMember.Local
        private void ConfigureElevatorServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IElevator, Elevator>();
            services.AddSingleton<IBuilding, Building>();
            services.AddSingleton<IEventStreamService, EventStreamService>();

            services.AddHostedService<GenerationPersonChecker>();
            services.AddHostedService<PersonGenerationService>();
            services.AddHostedService<ElevatorService>();
            services.AddHostedService<OutputService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();

                //endpoints.MapHub<TestHub>("/hubs/clock");
                //endpoints.MapHub<ElevatorHub>("/elevator");
            });
        }
    }
}