using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorClient.Services.Interfaces
{
    public interface IUiHelper 
    {
        Task ShowAlert(RenderFragment renderFragment, TimeSpan duration);
        Task ShowAlert(RenderFragment renderFragment);
        Task HideAlert();
    }
}
