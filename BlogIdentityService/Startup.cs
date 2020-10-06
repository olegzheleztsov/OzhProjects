using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Identity.Mongo;
using BlogIdentityService.Config;
using BlogIdentityService.Config.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BlogIdentityService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }
        
        private IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<JwtSettings>(Configuration.GetSection("JWTSettings"));
            services.Configure<Admin>(Configuration.GetSection("Admin"));
            
            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
            
            services.AddIdentityMongoDbProvider<ApplicationUser, ApplicationRole>(identityOptions =>
            {
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireDigit = false;
            }, mongoIdentityOptions =>
            {
                mongoIdentityOptions.ConnectionString = Configuration.GetValue<string>("CosmosDbClient:ConnectionString");
            });

            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });
            
            services.AddControllers();
            services.AddScoped<IRoleConfiguration, RoleConfiguration>();
            services.AddSingleton<IDefaultRoleSource, DefaultRoleSource>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRoleConfiguration roleConfiguration, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            /*
            roleConfiguration.CreateRolesAsync(serviceProvider.GetService<RoleManager<ApplicationRole>>()).Wait();
            var logger = serviceProvider.GetService<ILogger<Startup>>();
            var admin = Configuration.GetSection("Admin").Get<Admin>();
            logger.LogDebug(admin.ToString());
            
            LogEnvironmentValues(logger);
            */
            var admin = Configuration.GetSection("Admin").Get<Admin>();
            roleConfiguration.CreateAdministratorAsync(serviceProvider.GetService<UserManager<ApplicationUser>>(), admin).Wait();
        }

        private void LogEnvironmentValues(ILogger<Startup> logger)
        {
            logger.LogDebug($"{nameof(Environment.WebRootPath)}: {Environment.WebRootPath}");
            logger.LogDebug($"{nameof(Environment.ContentRootPath)}: {Environment.ContentRootPath}");

            var provider = Environment.ContentRootFileProvider;
            var contents = provider.GetDirectoryContents("");
            foreach (var fileInfo in contents)
            {
                logger.LogDebug($"{fileInfo.Name}, is directory: {fileInfo.IsDirectory}, path: {fileInfo.PhysicalPath}");
                if (fileInfo.IsDirectory)
                {
                    continue;
                }

                using var stream =  fileInfo.CreateReadStream();
                using var reader = new StreamReader(stream);
                try
                {
                    string text = reader.ReadToEnd();
                    logger.LogDebug(text);
                }
                catch (Exception ex)
                {
                    logger.LogError("Read error");
                }
            }
        }
    }
}