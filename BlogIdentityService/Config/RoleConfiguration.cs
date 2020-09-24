// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: RoleConfiguration.cs    Created at 2020/09/21/2:12 AM
// All rights reserved, for personal using only
// 

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Mongo.Stores;
using Microsoft.AspNetCore.Identity;

namespace BlogIdentityService.Config
{
    public class RoleConfiguration : IRoleConfiguration
    {
        
        /// <inheritdoc />
        public async Task CreateRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            List<ApplicationRole> roleList = new List<ApplicationRole>()
            {
                new ApplicationRole()
                {
                    Name = "Viewer",
                    NormalizedName = "VIEWER"
                },
                new ApplicationRole()
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            };

            foreach (var role in roleList)
            {
                if (!(await roleManager.RoleExistsAsync(role.Name).ConfigureAwait(false)))
                {
                    await roleManager.CreateAsync(role).ConfigureAwait(false);
                }
            }
            /*
            var viewerRole = await roleStore.FindByNameAsync("Viewer", CancellationToken.None).ConfigureAwait(false);
            if (viewerRole == null)
            {
                var result = await roleStore.CreateAsync(new ApplicationRole()
                {
                    Name = "Viewer",
                    NormalizedName = "VIEWER"
                }, CancellationToken.None).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    throw new Exception($"Cannot create role Viewer");
                }
            }
            
            var adminRole = await roleStore.FindByNameAsync("Administrator", CancellationToken.None).ConfigureAwait(false);
            if (adminRole == null)
            {
                var result = await roleStore.CreateAsync(new ApplicationRole()
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }, CancellationToken.None).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    throw new Exception($"Cannot create role Administrator");
                }
            }    
            */
        }

        public async Task CreateAdministratorAsync(UserManager<ApplicationUser> userManager)
        {
            string adminEmail = "zheleztsovoleh@gmail.com";
            string adminPassword = "87e898AA";
            
            var admin = await userManager.FindByEmailAsync(adminEmail).ConfigureAwait(false);
            if (admin == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                IdentityResult result = await userManager.CreateAsync(user, adminPassword).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator").ConfigureAwait(false);
                }
                else
                {
                    //throw new Exception("Cannot create admin user");
                }
            }
        }
    }
}