// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: TestAuthStateProvider.cs    Created at 2020/09/20/8:36 PM
// All rights reserved, for personal using only
// 

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace ElevatorClient.Authorization
{
    public class TestAuthStateProvider : AuthenticationStateProvider
    {
        /// <inheritdoc />
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(1500).ConfigureAwait(false);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "John Doe"),
                new Claim(ClaimTypes.Role, "Administrator")
            };
            
            var anonymous = new ClaimsIdentity();
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
        }
    }
}