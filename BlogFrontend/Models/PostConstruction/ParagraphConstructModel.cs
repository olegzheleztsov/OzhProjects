// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: ParagraphConstructModel.cs    Created at 2020/09/10/1:07 AM
// All rights reserved, for personal using only
// 

using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Models.PostConstruction
{
    public class ParagraphConstructModel : ElementConstructModel
    {
        /// <inheritdoc />
        public override PostElementType PostElementType => PostElementType.Paragraph;

        /// <inheritdoc />
        public override MarkupString ToMarkup()
        {
            return (MarkupString) $@"<p>{Value}</p>";
        }
    }
}