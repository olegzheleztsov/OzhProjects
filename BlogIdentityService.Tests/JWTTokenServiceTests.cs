// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService.Tests     File: JWTTokenServiceTests.cs    Created at 2020/10/06/11:22 AM
// All rights reserved, for personal using only
// 

using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogIdentityService.Config;
using BlogIdentityService.Config.Interfaces;
using BlogIdentityService.Services;
using Newtonsoft.Json;
using Xunit;

namespace BlogIdentityService.Tests
{
    public sealed class JwtTokenServiceTests
    {
        [Fact]
        public async Task Should_Generate_Token_Without_Errors()
        {
            var settings = await ReadSettingsFromFileAsync("secrets.json").ConfigureAwait(false);
            var claims = GenerateClaims();

            var service = new JWTTokenService();
            var token = await service.GenerateTokenAsync(claims, settings);
            Assert.NotNull(token);
            Assert.True(token.Length > 0);
        }

        [Fact]
        public async Task Should_Validate_Token_Correctly_When_Token_Valid()
        {
            var settings = await ReadSettingsFromFileAsync("secrets.json").ConfigureAwait(false);
            var claims = GenerateClaims();

            var service = new JWTTokenService();
            var token = await service.GenerateTokenAsync(claims, settings);

            Assert.True(await service.ValidateTokenAsync(token, settings));
        }

        [Fact]
        public async Task Should_Fail_Validation_When_Token_Incorrect()
        {
            var settings = await ReadSettingsFromFileAsync("secrets.json").ConfigureAwait(false);

            var claims = GenerateClaims();
            var service = new JWTTokenService();
            var token = await service.GenerateTokenAsync(claims, settings);

            var invalidSettings = await ReadSettingsFromFileAsync("invalid_secrets.json").ConfigureAwait(false);
            Assert.False(await service.ValidateTokenAsync(token, invalidSettings));
        }


        private static IEnumerable<Claim> GenerateClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "zheleztsovoleh@gmail.com"),
                new Claim(ClaimTypes.Role, "Administrator")
            };
            return claims;
        }

        private static async Task<IJwtSettings> ReadSettingsFromFileAsync(string fileName)
        {
            using var reader = new StreamReader(fileName);
            var jsonText = await reader.ReadToEndAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<JwtSettings>(jsonText);
        }
    }
}