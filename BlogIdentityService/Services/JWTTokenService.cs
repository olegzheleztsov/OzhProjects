// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: JWTTokenService.cs    Created at 2020/09/24/12:16 PM
// All rights reserved, for personal using only
// 

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogIdentityService.Config.Interfaces;
using BlogIdentityService.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BlogIdentityService.Services
{
    public class JWTTokenService : IJWTTokenService
    {
        /// <inheritdoc />
        public async Task<string> GenerateTokenAsync(IEnumerable<Claim> claims, IJwtSettings settings)
        {
            var secret = GenerateSecret(settings.SecurityKey);
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(settings.ValidIssuer, settings.ValidAudience, claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(settings.ExpiryInMinutes.ToString())),
                signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return await Task.FromResult(token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> ValidateTokenAsync(string token, IJwtSettings settings)
        {
            var secret = GenerateSecret(settings.SecurityKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = settings.ValidIssuer,
                    ValidAudience = settings.ValidAudience,
                    IssuerSigningKey = secret
                }, out var validatedToken);
            }
            catch(Exception exception)
            {
                return await Task.FromResult(false).ConfigureAwait(false);
            }

            return await Task.FromResult(true).ConfigureAwait(false);
        }

        private static SymmetricSecurityKey GenerateSecret(string securityKey)
        {
            var securityKeyBytes = Encoding.UTF8.GetBytes(securityKey);
            var secret = new SymmetricSecurityKey(securityKeyBytes);
            return secret;
        }
    }
}