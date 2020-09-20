// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: LinkConstructModel.cs    Created at 2020/09/10/1:18 AM
// All rights reserved, for personal using only
// 

using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Models.PostConstruction
{
    public class LinkConstructModel : ElementConstructModel
    {
        public string Content { get; set; } = string.Empty;
        /// <inheritdoc />
        public override PostElementType PostElementType => PostElementType.Link;

        /// <inheritdoc />
        public override MarkupString ToMarkup()
        {
            return (MarkupString) $"<a href=\"{Value}\">{Content}</a>";
        }
    }
}