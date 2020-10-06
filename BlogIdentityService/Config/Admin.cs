// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: Admin.cs    Created at 2020/09/24/5:20 AM
// All rights reserved, for personal using only
// 

using Newtonsoft.Json;

namespace BlogIdentityService.Config
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Admin
    {
        public string Email { get; set; }
        public string Password { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}