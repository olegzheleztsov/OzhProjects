// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: IAuthenticationService.cs    Created at 2020/09/20/11:11 PM
// All rights reserved, for personal using only
// 

using System.Threading.Tasks;
using ElevatorLib.Auth;

namespace ElevatorClient.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistrationDto);

        Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthenticationDto);

        Task Logout();
    }
}