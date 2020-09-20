using System.Collections.Generic;
using System.Threading.Tasks;
using ElevatorClient.Services.Interfaces;
using ElevatorLib.Models.Blogs;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Pages
{
    public partial class StartPage : ComponentBase
    {
        private IEnumerable<BlogDto> _blogs;

        [Inject] private IBlogService BlogService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            _blogs = await BlogService.GetBlogs().ConfigureAwait(false);
        }
    }
}