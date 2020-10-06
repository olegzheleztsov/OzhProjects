// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: IRoleConfiguration.cs    Created at 2020/09/21/2:21 AM
// All rights reserved, for personal using only
// 

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlogIdentityService.Config.Interfaces
{
    public interface IRoleConfiguration
    {
        Task CreateRolesAsync(RoleManager<ApplicationRole> roleStore);

        Task CreateAdministratorAsync(UserManager<ApplicationUser> userManager, Admin admin);
    }
}