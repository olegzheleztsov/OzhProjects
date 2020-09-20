// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: ElementModelSelection.razor.cs    Created at 2020/09/13/10:49 PM
// All rights reserved, for personal using only
// 

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Components.PostConstruction
{
    public partial class ElementModelSelection : ComponentBase
    {
        private string _selectedModel = string.Empty;

        [Inject] private IConstructModelDescriptor ConstructModelDescriptor { get; set; }

        [Parameter] public EventCallback<Type> ConstructionModelSelected { get; set; }

        private IEnumerable<string> ModelTypes => ConstructModelDescriptor.ModelLabels;

        private async Task OnAddElement()
        {
            if (string.IsNullOrEmpty(_selectedModel))
                throw new ArgumentException($"Selected model has wrong value: {_selectedModel}");

            var modelType = ConstructModelDescriptor.GetModelType(_selectedModel);
            await ConstructionModelSelected.InvokeAsync(modelType).ConfigureAwait(false);
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}