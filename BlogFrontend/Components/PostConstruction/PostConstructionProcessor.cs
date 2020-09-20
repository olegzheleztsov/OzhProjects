// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: PostConstructionProcessor.cs    Created at 2020/09/10/1:45 AM
// All rights reserved, for personal using only
// 

using System;
using System.Collections.Generic;
using ElevatorClient.Models.PostConstruction;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Components.PostConstruction
{
    internal sealed class PostConstructionProcessor : IPostProcessor
    {
        private readonly Dictionary<Type, Func<ElementConstructModel, RenderFragment>> _conversionMap;

        public PostConstructionProcessor()
        {
            _conversionMap =
                new Dictionary<Type, Func<ElementConstructModel, RenderFragment>>
                {
                    [typeof(BlockQuoteConstructModel)] = model => BuildFallback(model, "Blockquote"),
                    [typeof(ImageConstructModel)] = model => BuildFallback(model, "Image"),
                    [typeof(ParagraphConstructModel)] = model => BuildFallback(model, "Paragraph"),
                    [typeof(SectionHeadingConstructModel)] = model => BuildFallback(model, "Section"),
                    [typeof(SpanConstructModel)] = model => BuildFallback(model, "Span"),
                    [typeof(LinkConstructModel)] = BuildLink
                };
        }

        public (ElementConstructModel model, RenderFragment renderFragment) Process(Type modelType)
        {
            var model = Activator.CreateInstance(modelType);
            if (!_conversionMap.ContainsKey(model.GetType())) throw new ArgumentException(nameof(model));
            var renderFragment =  _conversionMap[model.GetType()](model as ElementConstructModel);
            return (model as ElementConstructModel, renderFragment);
        }

        /// <inheritdoc />
        public event EventHandler<ElementConstructModel> ModelChanged;

        private  RenderFragment BuildLink(ElementConstructModel model)
        {
            if (!(model is LinkConstructModel)) throw new ArgumentException(model.GetType().FullName);

            RenderFragment RenderFragment(LinkConstructModel internalModel)
            {
                return builder =>
                {
                    var index = 0;
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "mat-elevation-z24");
                    
                    builder.AddContent(index++, BuildFallback(internalModel, "Link"));
                    builder.OpenElement(index, "p");
                    builder.AddAttribute(index++, "class", "mat-subtitle2");
                    builder.AddContent(index++, "Link Content");
                    builder.CloseElement();

                    builder.OpenElement(index++, "input");
                    builder.AddAttribute(index++, "type", "input");
                    builder.AddAttribute(index++, "value", internalModel.Content);
                    
                    builder.AddAttribute(index++, "onchange",
                        EventCallback.Factory.CreateBinder(internalModel, value =>
                            {
                                internalModel.Content = value;
                                ModelChanged?.Invoke(this, internalModel);
                            },
                            internalModel.Content));
                    builder.SetUpdatesAttributeName("value");
                    
                    builder.AddAttribute(index++, "oninput",
                        EventCallback.Factory.CreateBinder(internalModel, value =>
                            {
                                internalModel.Content = value;
                                ModelChanged?.Invoke(this, internalModel);
                            },
                            internalModel.Content));
                    builder.SetUpdatesAttributeName("value");
                    
                    builder.CloseElement();
                    builder.CloseElement();
                    
                    builder.AddMarkupContent(index, "<hr />");
                };
            }

            return RenderFragment((LinkConstructModel) model);
        }

        private  RenderFragment BuildFallback(ElementConstructModel model, string modelName)
        {
            RenderFragment RenderFragment(ElementConstructModel internalModel)
            {
                return builder =>
                {
                    var index = 0;
                    
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "mat-elevation-z7");
                    
                    builder.OpenElement(index++, "p");
                    builder.AddAttribute(index++, "class", "mat-subtitle2");
                    builder.AddContent(index++, $"{modelName}:");
                    builder.CloseElement();
                    builder.OpenElement(index++, "input");
                    builder.AddAttribute(index++, "type", "input");
                    builder.AddAttribute(index++, "value", internalModel.Value);
                    
                    builder.AddAttribute(index++, "onchange",
                        EventCallback.Factory.CreateBinder(internalModel, value =>
                            {
                                internalModel.Value = value;
                                ModelChanged?.Invoke(this, internalModel);
                            },
                            internalModel.Value));
                    builder.SetUpdatesAttributeName("value");
                    
                    builder.AddAttribute(index++, "oninput",
                        EventCallback.Factory.CreateBinder(internalModel, value =>
                            {
                                internalModel.Value = value;
                                ModelChanged?.Invoke(this, internalModel);
                            },
                            internalModel.Value));
                    builder.SetUpdatesAttributeName("value");
                    
                    builder.CloseElement();
                    builder.CloseElement();
                    builder.AddMarkupContent(index, "<hr />");
                };
            }

            return RenderFragment(model);
        }
    }
}