// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: IJwtSettings.cs    Created at 2020/10/06/11:28 AM
// All rights reserved, for personal using only
// 

namespace BlogIdentityService.Config.Interfaces
{
    public interface IJwtSettings
    {
        string SecurityKey { get; }

        string ValidIssuer { get; }

        string ValidAudience { get; }

        int ExpiryInMinutes { get; }
    }
}