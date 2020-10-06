// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: IJWTTokenService.cs    Created at 2020/09/24/11:59 AM
// All rights reserved, for personal using only
// 

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogIdentityService.Config.Interfaces;

namespace BlogIdentityService.Services.Interfaces
{
    public interface IJWTTokenService
    {
        Task<string> GenerateTokenAsync(IEnumerable<Claim> claims, IJwtSettings settings);
        Task<bool> ValidateTokenAsync(string token, IJwtSettings settings);
    }
}