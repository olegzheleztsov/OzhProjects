// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: ApplicationUserDto.cs    Created at 2020/09/20/10:30 PM
// All rights reserved, for personal using only
// 

using System;

namespace BlogIdentityService
{
    public class ApplicationUserDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}