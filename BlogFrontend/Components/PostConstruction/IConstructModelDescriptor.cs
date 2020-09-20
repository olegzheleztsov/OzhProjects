// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: IConstructModelDescriptor.cs    Created at 2020/09/13/1:05 PM
// All rights reserved, for personal using only
// 

using System;
using System.Collections.Generic;

namespace ElevatorClient.Components.PostConstruction
{
    public interface IConstructModelDescriptor
    {
        IEnumerable<string> ModelLabels { get; }

        Type GetModelType(string modelLabel);
    }
}