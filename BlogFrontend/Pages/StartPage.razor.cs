using System.Collections.Generic;
using System.Threading.Tasks;
using ElevatorClient.Services.Interfaces;
using ElevatorLib.Models.Blogs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ElevatorClient.Pages
{
    public partial class StartPage : ComponentBase
    {
        private IEnumerable<BlogDto> _blogs;

        [Inject] private IBlogService BlogService { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        private string _authOutput = string.Empty;
        private int _counter = 0;
        
        private async Task OnSampleAuthAccess()
        {
            var authState = await AuthState;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                _counter++;
                _authOutput =
                    $"Authenticated: {user.Identity.Name}, Auth Type: {user.Identity.AuthenticationType}, Counter: {_counter}";
            }
            else
            {
                _authOutput = $"Not authenticated: {_counter}";
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _blogs = await BlogService.GetBlogs().ConfigureAwait(false);
        }
    }
}