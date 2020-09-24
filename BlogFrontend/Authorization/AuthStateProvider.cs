// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: AuthStateProvider.cs    Created at 2020/09/21/1:22 AM
// All rights reserved, for personal using only
// 

using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ElevatorClient.Configs;
using ElevatorClient.Features;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;

namespace ElevatorClient.Authorization
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;

        public AuthStateProvider(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage,
            IOptions<AuthServiceConfiguration> options)
        {
            _httpClient = httpClientFactory.CreateClient(options.Value.AuthHttpClientName);
            _localStorage = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }


        /// <inheritdoc />
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrWhiteSpace(token))
            {
                return _anonymous;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }

        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}