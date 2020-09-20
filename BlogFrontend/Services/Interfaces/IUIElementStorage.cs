using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorClient.Services.Interfaces
{
    public interface IUiElementStorage
    {
        void AddElement<T>(T uiElement) where T : ComponentBase;
        bool RemoveElement<T>() where T : ComponentBase;

        T GetElement<T>() where T : ComponentBase;
    }
}
