// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: BlockQuoteConstructModel.cs    Created at 2020/09/10/1:15 AM
// All rights reserved, for personal using only
// 

using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Models.PostConstruction
{
    public class BlockQuoteConstructModel : ElementConstructModel
    {

        /// <inheritdoc />
        public override PostElementType PostElementType => PostElementType.BlockQuote;

        /// <inheritdoc />
        public override MarkupString ToMarkup()
        {
            return (MarkupString) $"<blockquote class=\"blockquote\">{Value}</blockquote>";
        }
    }
}