// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: ElementConstructModel.cs    Created at 2020/09/10/1:10 AM
// All rights reserved, for personal using only
// 

using System;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Models.PostConstruction
{
    public abstract class ElementConstructModel
    {
        public abstract PostElementType PostElementType { get; }

        public string Value { get; set; } = string.Empty;
        public abstract MarkupString ToMarkup();
        
    }
}