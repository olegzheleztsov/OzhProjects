using ElevatorClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorClient.Services
{
    public class UiElementStorage : IUiElementStorage
    {
        private readonly Dictionary<Type, object> _elements = new Dictionary<Type, object>();
        private ILogger<UiElementStorage> _logger;

        public UiElementStorage(ILogger<UiElementStorage> logger)
        {
            _logger = logger;
        }

        public void AddElement<T>(T uiElement) where T : ComponentBase
        {
            if (_elements.ContainsKey(typeof(T)))
            {
                if(!RemoveElement<T>())
                {
                    throw new InvalidOperationException($"Unable to remove element of type: {typeof(T)}");
                }
            }
            _elements.Add(typeof(T), uiElement);
            _logger.LogInformation("Element of type {type} added", typeof(T).FullName);
        }

        public T GetElement<T>() where T : ComponentBase
        {
            if(_elements.ContainsKey(typeof(T)))
            {
                return _elements[typeof(T)] as T;
            }
            _logger.LogInformation("Not found element of type: {type}", typeof(T).FullName);
            return default;
        }

        public bool RemoveElement<T>() where T : ComponentBase
        {
            var result = _elements.Remove(typeof(T));
            _logger.LogInformation("Element of type {type} removed with status: {result}", typeof(T).FullName, result);
            return result;
        }
    }
}
