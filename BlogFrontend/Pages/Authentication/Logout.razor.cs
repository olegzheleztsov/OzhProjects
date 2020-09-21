// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: Logout.razor.cs    Created at 2020/09/21/1:59 AM
// All rights reserved, for personal using only
// 

using System.Threading.Tasks;
using ElevatorClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Pages.Authentication
{
    public partial class Logout : ComponentBase
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}