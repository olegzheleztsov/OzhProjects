using System;
using Bunit;
using ElevatorClient.Components.Misc;
using Xunit;

namespace BlogFrontend.Tests
{
    public class HelloWorldTest
    {
        [Fact]
        public void HelloWorldComponentRendersCorrectly()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<HelloWorld>();
            cut.MarkupMatches("<h1>Hello world from Blazor</h1>");
        }
    }
}