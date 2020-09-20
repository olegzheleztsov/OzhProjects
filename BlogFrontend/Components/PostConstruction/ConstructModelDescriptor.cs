// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: ConstructModelDescriptor.cs    Created at 2020/09/13/1:06 PM
// All rights reserved, for personal using only
// 

using System;
using System.Collections.Generic;
using System.Linq;
using ElevatorClient.Models.PostConstruction;

namespace ElevatorClient.Components.PostConstruction
{
    public class ConstructModelDescriptor : IConstructModelDescriptor
    {
        private static readonly Dictionary<string, Type> ModelTypes = new Dictionary<string, Type>()
        {
            ["Blockquote"] = typeof(BlockQuoteConstructModel),
            ["Image"] = typeof(ImageConstructModel),
            ["Link"] = typeof(LinkConstructModel),
            ["Paragraph"] = typeof(ParagraphConstructModel),
            ["Section"] = typeof(SectionHeadingConstructModel),
            ["Span"] = typeof(SpanConstructModel)
        };

        /// <inheritdoc />
        public IEnumerable<string> ModelLabels => ModelTypes.Keys.OrderBy(k => k);

        /// <inheritdoc />
        public Type GetModelType(string modelLabel)
        {
            if (ModelTypes.TryGetValue(modelLabel, out var modelType))
            {
                return modelType;
            } 
            throw new ArgumentException($"Invalid value for: {nameof(modelLabel)}: {modelLabel}");
        }
    }
}