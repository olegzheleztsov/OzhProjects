// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: IDefaultRoleSource.cs    Created at 2020/09/24/6:00 AM
// All rights reserved, for personal using only
// 

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogIdentityService.Config.Interfaces
{
    public interface IDefaultRoleSource
    {
        Task<IEnumerable<ApplicationRole>> GetDefaultRoles();
        Task<ApplicationRole> GetDefaultAdminRole();
        Task<ApplicationRole> GetRole(string name);
    }
}