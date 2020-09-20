using System.Threading.Tasks;
using ElevatorLib.Models.Blogs;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Components.Blogs
{
    public partial class PostPreview : ComponentBase
    {
        [Parameter] public BlogDto Blog { get; set; }
        
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Blog != null) Blog.SubTitle = Blog.Title;
            await Task.CompletedTask.ConfigureAwait(false);
        }

        private void OnPostLinkClicked()
        {
            NavigationManager.NavigateTo($"/post/{Blog.Id}");
        }
    }
}