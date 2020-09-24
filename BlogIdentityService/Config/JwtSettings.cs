// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: JwtSettings.cs    Created at 2020/09/21/1:01 AM
// All rights reserved, for personal using only
// 

namespace BlogIdentityService.Config
{
    public class JwtSettings
    {
        public string SecurityKey { get; set; }
        
        public string ValidIssuer { get; set; }
        
        public string ValidAudience { get; set; }
        
        public int ExpiryInMinutes { get; set; }
    }
}