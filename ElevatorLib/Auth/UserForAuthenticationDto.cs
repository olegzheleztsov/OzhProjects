// Create By: Oleg Gelezcov                        (olegg )
// Project: ElevatorLib     File: UserForAuthenticationDto.cs    Created at 2020/09/21/12:53 AM
// All rights reserved, for personal using only
// 

using System.ComponentModel.DataAnnotations;

namespace ElevatorLib.Auth
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}