using System;
using System.Threading.Tasks;
using ElevatorClient.Extensions;
using ElevatorClient.Services.Interfaces;
using ElevatorLib.Models.Blogs;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace ElevatorClient.Components.Blogs
{
    public partial class BlogComponent : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        private BlogDto Blog { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }
        
        [Inject] private IBlogService BlogService { get; set; }
        
        [Inject] private ILogger<BlogComponent> Logger { get; set; }

        private string BlogTitle => Blog?.Title ?? string.Empty;
        private string BlogSubTitle => Blog?.SubTitle ?? string.Empty;

        private string BlogAuthorInfo =>
            $"Posted by {Blog?.AuthorName ?? string.Empty} on {Blog?.Time.ToString() ?? string.Empty}";

        private MarkupString BlogContent => Blog?.Content?.ToMarkup() ?? new MarkupString(string.Empty);

        /// <inheritdoc />
        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                throw new ArgumentException(nameof(Id));
            }

            Blog = await BlogService.GetBlog(Id).ConfigureAwait(false);
            if (Blog == null)
            {
                throw new InvalidOperationException(nameof(Blog));
            }
        }

        private async Task OnEdit()
        {
            NavigationManager.NavigateTo($"/post/edit/{Id}");
            await Task.CompletedTask.ConfigureAwait(false);
        }

        private async Task OnDelete()
        {
            Logger.LogInformation("OnDelete()");
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}