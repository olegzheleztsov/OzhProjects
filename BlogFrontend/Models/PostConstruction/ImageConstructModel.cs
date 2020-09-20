// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: ImageConstructModel.cs    Created at 2020/09/10/1:16 AM
// All rights reserved, for personal using only
// 

using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Models.PostConstruction
{
    public class ImageConstructModel : ElementConstructModel
    {

        /// <inheritdoc />
        public override PostElementType PostElementType => PostElementType.Image;

        /// <inheritdoc />
        public override MarkupString ToMarkup()
        {
            return (MarkupString) $"<a href=\"#\"><img class=\"img-fluid\" src=\"{Value}\" alt=\"\"></a>";
        }
    }
}