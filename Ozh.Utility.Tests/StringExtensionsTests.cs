using System;
using Xunit;

namespace Ozh.Utility.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("http://google.com")]
        [InlineData("https://microsoft.com")]
        [InlineData("http://microsoft.dot.com")]
        [InlineData("https://azure.com/some/wey?id=1")]
        [InlineData("http://microsoft")]
        [InlineData("http://azure/com/hee.this")]
        public void These_Strings_Should_Be_Valid_Urls(string url)
        {
            Assert.True(url.IsValidUrl());
        }

        [Theory]
        [InlineData("tcp://apple.com")]
        [InlineData("http:/amazon.com")]
        [InlineData("google.com")]
        [InlineData("google")]
        [InlineData("")]
        public void These_Strings_Shouldnot_Be_Valid_Urls(string url)
        {
            Assert.False(url.IsValidUrl());
        }

    }
}
