// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: Registration.razor.cs    Created at 2020/09/20/11:29 PM
// All rights reserved, for personal using only
// 

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ElevatorClient.Services.Interfaces;
using ElevatorLib.Auth;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Pages.Authentication
{
    public partial class Registration : ComponentBase
    {
        private UserForRegistrationDto _userForRegistration = new UserForRegistrationDto();
        
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        public bool ShowRegistrationErros { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public async Task Register()
        {
            ShowRegistrationErros = false;
            var result = await AuthenticationService.RegisterUser(_userForRegistration);
            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowRegistrationErros = true;
                StateHasChanged();
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}