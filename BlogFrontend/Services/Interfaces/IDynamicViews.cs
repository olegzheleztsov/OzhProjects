// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: IDynamicViews.cs    Created at 2020/09/15/12:39 AM
// All rights reserved, for personal using only
// 

using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace ElevatorClient.Services.Interfaces
{
    public interface IDynamicViews
    {
        Action<RenderTreeBuilder> GetAlertErrorView(string errorMessage);
    }
}