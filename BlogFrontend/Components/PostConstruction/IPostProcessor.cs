// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: IPostProcessor.cs    Created at 2020/09/13/12:44 PM
// All rights reserved, for personal using only
// 

using System;
using ElevatorClient.Models.PostConstruction;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Components.PostConstruction
{
    public interface IPostProcessor
    {
        (ElementConstructModel model, RenderFragment renderFragment) Process(Type modelType);

        event EventHandler<ElementConstructModel> ModelChanged;

    }
}