// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend.Tests     File: BlogEditorTests.cs    Created at 2020/09/17/1:36 AM
// All rights reserved, for personal using only
// 

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlogFrontend.Tests.Fakes;
using Bunit;
using Bunit.TestDoubles.JSInterop;
using ElevatorClient.Components.Blogs;
using ElevatorClient.Services.Interfaces;
using ElevatorLib.Models.Blogs;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Moq;

namespace BlogFrontend.Tests.Components.Blogs
{
    public class BlogEditorTests
    {
        [Fact]
        public void Should_Correctly_Change_Parameter_Value()
        {
            var blog = CreateBlog();
            var blogServiceMock = new Mock<IBlogService>();
            blogServiceMock.Setup(serv => serv.GetBlog(Moq.It.IsAny<string>())).Returns(Task.FromResult(blog));
            blogServiceMock.Setup(serv => serv.UpdateBlog(It.IsAny<string>(), It.IsAny<BlogDto>()))
                .Returns(Task.FromResult(blog));
            
            var uiHelperMock = new Mock<IUiHelper>();
            uiHelperMock.Setup(serv => serv.ShowAlert(It.IsAny<RenderFragment>(), It.IsAny<TimeSpan>())).Returns(Task.CompletedTask);
            
            var dynamicViewsMock = new Mock<IDynamicViews>();
            dynamicViewsMock.Setup(serv => serv.GetAlertErrorView(It.IsAny<string>())).Returns(obj => { });
            
            using var ctx = new TestContext();
            ctx.Services.AddScoped<IBlogService>(sp => blogServiceMock.Object);
            ctx.Services.AddScoped<IUiHelper>(sp => uiHelperMock.Object);
            ctx.Services.AddScoped<IDynamicViews>(sp => dynamicViewsMock.Object);
            ctx.Services.AddSingleton<NavigationManager>(sp => new FakeNavigationManager());
            ctx.Services.AddMockJSRuntime();
            
            var cut2 = ctx.RenderComponent<BlogEditor>(
                (nameof(BlogEditor.Blog), blog),
                (nameof(BlogEditor.Id), blog.Id));
            var realElement = cut2.Find("input[aria-label=Title]");
            Assert.Equal("My Title", realElement.Attributes.First(attr => attr.LocalName == "value").Value);
        }

        private BlogDto CreateBlog()
        {
            return new BlogDto()
            {
                AuthorName = "Oleh",
                Content = "My article",
                Id = "abc",
                Links = null,
                SubTitle = "SubTitle",
                Time = new DateTime(2000, 2, 2),
                Title = "My Title"
            };
        }
    }
}