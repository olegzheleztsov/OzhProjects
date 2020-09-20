using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ElevatorClient.Services.Interfaces;
using ElevatorLib.Models.Blogs;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Components.Blogs
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public sealed partial class BlogEditor : ComponentBase
    {
        private const int ERROR_ALERT_INTERVAL = 5;

        private MatTextField<string> _titleField;
        private MatTextField<string> _subTitleField;
        private MatTextField<string> _authorField;
        private MatTextField<string> _contentField;
        private BlogDto _cachedCopy = null;
        
        
        [Parameter] public BlogDto Blog { get; set; }

        [Parameter] public string Id { get; set; }

        [Inject] private IBlogService BlogService { get; set; }

        [Inject] private IUiHelper UiHelper { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        
        [Inject] private IDynamicViews DynamicViews { get; set; }

        private string PostPath => $"/post/{Id}";

        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(Id)) throw new ArgumentException(nameof(Id));
            Blog = await BlogService.GetBlog(Id).ConfigureAwait(false);
            if (Blog == null) throw new Exception(nameof(BlogService.GetBlog));
            _cachedCopy = Blog.Clone() as BlogDto;
        }

        private async Task OnValidSubmit()
        {
            Blog.Time = DateTime.UtcNow;
            var returnDto = await BlogService.UpdateBlog(Id, Blog).ConfigureAwait(false);
            if (returnDto == null) throw new Exception(nameof(BlogService.UpdateBlog));
            NavigationManager.NavigateTo(PostPath);
        }

        private async Task OnInvalidSubmit()
        {
            await UiHelper.ShowAlert( new RenderFragment(DynamicViews.GetAlertErrorView("You should correct all errors before submit")), TimeSpan.FromSeconds(ERROR_ALERT_INTERVAL)).ConfigureAwait(false);
        }

        private async Task OnDiscard()
        {
            if (string.IsNullOrEmpty(Id)) throw new ArgumentException(nameof(Id));
            Blog?.CopyFrom(_cachedCopy);
            NavigationManager.NavigateTo(PostPath);
            await Task.CompletedTask.ConfigureAwait(false);
        }

        private void OnKeyPress(EditFiledType editFiledType)
        {
            switch (editFiledType)
            {
                case EditFiledType.Author:
                    Blog.AuthorName = _authorField.Value;
                    break;
                case EditFiledType.Content:
                    Blog.Content = _contentField.Value;
                    break;
                case EditFiledType.Title:
                    Blog.Title = _titleField.Value;
                    break;
                case EditFiledType.SubTitle:
                    Blog.SubTitle = _subTitleField.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(editFiledType), editFiledType, null);
            }
        }
        
        private enum EditFiledType
        {
            Title, 
            SubTitle,
            Author,
            Content
        }
    }
}