using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ElevatorClient.Authorization;
using ElevatorClient.Components.PostConstruction;
using ElevatorClient.Configs;
using ElevatorClient.Services;
using ElevatorClient.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace ElevatorClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.Configure<BlogServiceConfiguration>(builder.Configuration.GetSection("BlogService"));
            builder.Services.Configure<AuthServiceConfiguration>(builder.Configuration.GetSection("AuthService"));

            string backendUri = builder.Configuration["BlogService:BackendUri"];
            string blogHttpClientName = builder.Configuration["BlogService:BlogHttpClientName"];
            var authServiceUri = builder.Configuration["AuthService:AuthUri"];
            var authHttpClientName = builder.Configuration["AuthService:AuthHttpClientName"];

            builder.Services.AddSingleton<IUiElementStorage, UiElementStorage>();
            builder.Services.AddScoped<IUiHelper, UiHelper>();
            builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddHttpClient(blogHttpClientName, client =>
            {
                client.BaseAddress = new Uri(backendUri);
            });
            builder.Services.AddHttpClient(authHttpClientName, client =>
            {
                client.BaseAddress = new Uri(authServiceUri);
            });
            

            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<IPostProcessor, PostConstructionProcessor>();
            builder.Services.AddScoped<IConstructModelDescriptor, ConstructModelDescriptor>();
            builder.Services.AddScoped<IDynamicViews, DynamicViews>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddBlazoredLocalStorage(config =>
            {
                config.JsonSerializerOptions.WriteIndented = true;
            });
            await builder.Build().RunAsync();
        }
    }
}
