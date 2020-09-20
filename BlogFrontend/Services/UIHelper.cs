using ElevatorClient.Components;
using ElevatorClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorClient.Services
{
    public class UiHelper : IUiHelper, IUiElementStorage
    {
        private readonly ILogger<UiHelper> _logger;
        private readonly IUiElementStorage _storage;

        public UiHelper(ILogger<UiHelper> logger, IUiElementStorage storage)
        {
            _logger = logger;
            _storage = storage;
        }


        public async Task HideAlert()
        {
            var alert = _storage.GetElement<Alert>();
            alert?.Hide();
            await Task.CompletedTask.ConfigureAwait(false);
        }

        public async Task ShowAlert(RenderFragment renderFragment, TimeSpan duration)
        {
            var alert = _storage.GetElement<Alert>();
            if(alert == null)
            {
                _logger.LogError($"Didn't find alert");
            } 
            await alert.ShowAsync(duration, renderFragment).ConfigureAwait(false);
        }

        public async Task ShowAlert(RenderFragment renderFragment)
        {
            var alert = _storage.GetElement<Alert>();
            alert?.Show(renderFragment);
            await Task.CompletedTask.ConfigureAwait(false);
        }

        public void AddElement<T>(T uiElement) where T : ComponentBase
        {
            _storage.AddElement<T>(uiElement);
        }

        public bool RemoveElement<T>() where T : ComponentBase
        {
            return _storage.RemoveElement<T>();
        }

        public T GetElement<T>() where T : ComponentBase
        {
            return _storage.GetElement<T>();
        }
    }
}
