// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: DynamicViews.cs    Created at 2020/09/15/12:40 AM
// All rights reserved, for personal using only
// 

using System;
using ElevatorClient.Services.Interfaces;
using Microsoft.AspNetCore.Components.Rendering;

namespace ElevatorClient.Services
{
    public class DynamicViews : IDynamicViews
    {
        /// <inheritdoc />
        public Action<RenderTreeBuilder> GetAlertErrorView(string errorMessage)
        {
            return (builder) =>
            {
                var index = 0;
                builder.OpenElement(index++, "p");
                builder.AddAttribute(index++, "class", "mat-h6");
                builder.AddAttribute(index++, "style", "color:red");
                builder.AddContent(index++, errorMessage);
                builder.CloseElement();
            };
        }
    }
}