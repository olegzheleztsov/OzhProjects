// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: PostConstructor.razor.cs    Created at 2020/09/13/10:40 PM
// All rights reserved, for personal using only
// 

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ElevatorClient.Components.PostConstruction;
using ElevatorClient.Models.PostConstruction;
using ElevatorClient.Services.Interfaces;
using ElevatorLib.Models.Blogs;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Pages
{
    public partial class PostConstructor : ComponentBase
    {
        private readonly IList<(ElementConstructModel model, RenderFragment renderFragment)> _modelViews =
            new List<(ElementConstructModel model, RenderFragment renderFragment)>();

        private PostBaseInfoInput _postBaseInfoInput;

        [Inject] private IPostProcessor PostProcessor { get; set; }

        [Inject] private IBlogService BlogService { get; set; }


        private void OnModelSelected(Type elementModelType)
        {
            var (model, renderFragment) = PostProcessor.Process(elementModelType);
            _modelViews.Add((model, renderFragment));
            StateHasChanged();
        }

        private void OnItemRemoved(ElementConstructModel model)
        {
            var index = _modelViews.FindIndex(item => item.model == model);
            if (index >= 0)
                _modelViews.RemoveAt(index);
            else
                throw new ArgumentException("Didn't find model");
        }

        private async Task OnSavePost()
        {
            var postStringBuilder = new StringBuilder();
            foreach (var (model, view) in _modelViews) postStringBuilder.Append(model.ToMarkup().Value);

            var baseInfo = _postBaseInfoInput.PostBaseInfo;
            var blog = new BlogDto
            {
                Title = baseInfo.Title,
                SubTitle = baseInfo.SubTitle,
                AuthorName = baseInfo.Author,
                Content = postStringBuilder.ToString(),
                Time = DateTime.UtcNow
            };
            await BlogService.CreateBlog(blog).ConfigureAwait(false);
        }
    }
}