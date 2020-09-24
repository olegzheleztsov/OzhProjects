// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: Login.razor.cs    Created at 2020/09/21/1:55 AM
// All rights reserved, for personal using only
// 

using System.Threading.Tasks;
using ElevatorClient.Services.Interfaces;
using ElevatorLib.Auth;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Pages.Authentication
{
    public partial class Login : ComponentBase
    {
        private UserForAuthenticationDto _userForAuthentication = new UserForAuthenticationDto();
        
        [Inject] public IAuthenticationService AuthenticationService { get; set; }
        
        [Inject] public NavigationManager NavigationManager { get; set; }
        
        public bool ShowAuthError { get; set; }
        
        public string Error { get; set; }

        public async Task ExecuteLogin()
        {
            ShowAuthError = false;

            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}