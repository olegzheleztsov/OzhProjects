// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: SectionHeadingConstructModel.cs    Created at 2020/09/10/1:13 AM
// All rights reserved, for personal using only
// 

using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Models.PostConstruction
{
    public class SectionHeadingConstructModel : ElementConstructModel
    {
        /// <inheritdoc />
        public override PostElementType PostElementType => PostElementType.SectionHeading;

        /// <inheritdoc />
        public override MarkupString ToMarkup()
        {
            return (MarkupString) $"<h2 class=\"section-heading\">{Value}</h2>";
        }
    }
}