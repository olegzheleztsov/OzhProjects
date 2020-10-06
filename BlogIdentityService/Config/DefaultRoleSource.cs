// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: DefaultRoleSource.cs    Created at 2020/09/24/5:28 AM
// All rights reserved, for personal using only
// 

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogIdentityService.Config.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace BlogIdentityService.Config
{
    public class DefaultRoleSource : IDefaultRoleSource
    {
        private readonly IWebHostEnvironment _environment;
        private IEnumerable<ApplicationRole> _defaultRoles;

        private const string AdministratorRoleName = "Administrator";
        
        public DefaultRoleSource(IWebHostEnvironment environment)
        {
            _environment = environment;
            _defaultRoles = null;
        }


        private async Task ReadDefaultRoles()
        {
            var defaultRolesFile =
                _environment.ContentRootFileProvider.GetFileInfo(Path.Combine("wwwroot", "default_roles.json"));
            await using var fileStream = defaultRolesFile.CreateReadStream();
            using var reader = new StreamReader(fileStream);
            var fileContent = await reader.ReadToEndAsync().ConfigureAwait(false);
            _defaultRoles = JsonConvert.DeserializeObject<List<ApplicationRole>>(fileContent);
        }

        private async Task TryReadDefaultRoles()
        {
            static void ApplyNameNormalization(IEnumerable<ApplicationRole> roles)
            {
                foreach (var role in roles)
                {
                    role.NormalizedName = role.Name.ToUpper();
                }
            }
            
            if (_defaultRoles == null)
            {
                await ReadDefaultRoles().ConfigureAwait(false);
                ApplyNameNormalization(_defaultRoles);
            }
            Debug.Assert(_defaultRoles != null);
        }


        
        public async Task<IEnumerable<ApplicationRole>> GetDefaultRoles()
        {
            await TryReadDefaultRoles().ConfigureAwait(false);
            return _defaultRoles;
        }

        public async Task<ApplicationRole> GetDefaultAdminRole()
        {
            await TryReadDefaultRoles().ConfigureAwait(false);
            return _defaultRoles.FirstOrDefault(role => role.Name == AdministratorRoleName);
        }

        public async Task<ApplicationRole> GetRole(string name)
        {
            await TryReadDefaultRoles().ConfigureAwait(false);
            return _defaultRoles.FirstOrDefault(role => role.Name == name);
        }
    }
}