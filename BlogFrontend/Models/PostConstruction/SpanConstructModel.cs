// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: SpanConstructModel.cs    Created at 2020/09/10/1:17 AM
// All rights reserved, for personal using only
// 

using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Models.PostConstruction
{
    public class SpanConstructModel : ElementConstructModel
    {
        /// <inheritdoc />
        public override PostElementType PostElementType => PostElementType.Span;

        /// <inheritdoc />
        public override MarkupString ToMarkup()
        {
            return (MarkupString) $"<span class=\"caption text-muted\">{Value}</span>";
        }
    }
}