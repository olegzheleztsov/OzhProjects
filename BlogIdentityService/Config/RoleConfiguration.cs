// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: RoleConfiguration.cs    Created at 2020/09/21/2:12 AM
// All rights reserved, for personal using only
// 

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Mongo.Stores;
using BlogIdentityService.Config.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogIdentityService.Config
{
    public class RoleConfiguration : IRoleConfiguration
    {
        
        private readonly IDefaultRoleSource _defaultRoleSource;

        public RoleConfiguration(IDefaultRoleSource source)
        {
            _defaultRoleSource = source;
        }
        
        /// <inheritdoc />
        public async Task CreateRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            var roles = await _defaultRoleSource.GetDefaultRoles().ConfigureAwait(false);
            foreach (var role in roles)
            {
                if (!(await roleManager.RoleExistsAsync(role.Name).ConfigureAwait(false)))
                {
                    await roleManager.CreateAsync(role).ConfigureAwait(false);
                }
            }
        }

        public async Task CreateAdministratorAsync(UserManager<ApplicationUser> userManager, Admin adminConfig)
        {
            var admin = await userManager.FindByEmailAsync(adminConfig.Email).ConfigureAwait(false);
            if (admin == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = adminConfig.Email,
                    Email = adminConfig.Email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, adminConfig.Password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    var adminRole = await _defaultRoleSource.GetDefaultAdminRole().ConfigureAwait(false);
                    await userManager.AddToRoleAsync(user, adminRole.Name).ConfigureAwait(false);
                }
            }
        }
    }
}