// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: StringExtensions.cs    Created at 2020/09/14/11:16 PM
// All rights reserved, for personal using only
// 

using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Extensions
{
    public static class StringExtensions
    {
        public static MarkupString ToMarkup(this string content) => new MarkupString(content);
    }
}