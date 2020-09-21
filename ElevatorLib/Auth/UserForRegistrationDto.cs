// Create By: Oleg Gelezcov                        (olegg )
// Project: ElevatorLib     File: UserForRegistrationDto.cs    Created at 2020/09/20/11:01 PM
// All rights reserved, for personal using only
// 

using System.ComponentModel.DataAnnotations;

namespace ElevatorLib.Auth
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}