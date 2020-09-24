// Create By: Oleg Gelezcov                        (olegg )
// Project: ElevatorLib     File: AuthResponseDto.cs    Created at 2020/09/21/12:54 AM
// All rights reserved, for personal using only
// 

namespace ElevatorLib.Auth
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}