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


        
        private async Task OnSampleAuthAccess()
        {
            var authState = await AuthState;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {

            }
            else
            {

            }
        }

        protected override async Task OnInitializedAsync()
        {
            _blogs = await BlogService.GetBlogs().ConfigureAwait(false);
        }
    }
}