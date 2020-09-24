// Create By: Oleg Gelezcov                        (olegg )
// Project: ElevatorLib     File: RegistrationResponseDto.cs    Created at 2020/09/20/11:03 PM
// All rights reserved, for personal using only
// 

using System.Collections.Generic;

namespace ElevatorLib.Auth
{
    public class RegistrationResponseDto
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}