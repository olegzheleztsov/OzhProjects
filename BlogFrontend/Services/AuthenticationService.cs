// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: AuthenticationService.cs    Created at 2020/09/20/11:12 PM
// All rights reserved, for personal using only
// 

using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ElevatorClient.Authorization;
using ElevatorClient.Configs;
using ElevatorClient.Services.Interfaces;
using ElevatorLib.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ElevatorClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(IHttpClientFactory httpClientFactory, 
            IOptions<AuthServiceConfiguration> options, ILogger<AuthenticationService> logger, 
            AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _client = httpClientFactory.CreateClient(options.Value.AuthHttpClientName);
            _logger = logger;
            _authStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        /// <inheritdoc />
        public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var registrationResult = await _client
                .PostAsJsonAsync<UserForRegistrationDto>("account/registration", userForRegistrationDto).ConfigureAwait(false);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            
            _logger.LogInformation(registrationContent);
            
            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }

            return new RegistrationResponseDto() {IsSuccessfulRegistration = true};
        }

        /// <inheritdoc />
        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthenticationDto)
        {
            var authResult = await _client
                .PostAsJsonAsync<UserForAuthenticationDto>("account/login", userForAuthenticationDto)
                .ConfigureAwait(false);
            var result = await authResult.Content.ReadFromJsonAsync<AuthResponseDto>().ConfigureAwait(false);
            if (!authResult.IsSuccessStatusCode)
            {
                return result;
            }

            await _localStorage.SetItemAsync("authToken", result.Token).ConfigureAwait(false);
            (_authStateProvider as AuthStateProvider)?.NotifyUserAuthentication(result.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return new AuthResponseDto() {IsAuthSuccessful = true};
        }

        /// <inheritdoc />
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken").ConfigureAwait(false);
            (_authStateProvider as AuthStateProvider)?.NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}