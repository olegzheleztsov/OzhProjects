// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: ElementConstructionView.razor.cs    Created at 2020/09/13/10:48 PM
// All rights reserved, for personal using only
// 

using System.Threading.Tasks;
using ElevatorClient.Models.PostConstruction;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

namespace ElevatorClient.Components.PostConstruction
{
    public partial class ElementConstructionView : ComponentBase
    {
            [Parameter]
            public ElementConstructModel Model { get; set; }
            
            
            [Parameter]
            public RenderFragment ChildContent { get; set; }
            
            [Inject]
            private IPostProcessor PostProcessor { get; set; }
            
            [Parameter]
            public EventCallback<ElementConstructModel> ItemRemoved { get; set; }
        
            private RenderFragment _modelPreviewFragment = new RenderFragment(builder => builder.AddContent(0, (MarkupString)"<div></div>"));
                
            private async Task OnRemoveItem(MouseEventArgs obj)
            {
                await ItemRemoved.InvokeAsync(Model).ConfigureAwait(false);
            }
        
            /// <inheritdoc />
            protected override Task OnInitializedAsync()
            {
                if (PostProcessor != null)
                {
                    PostProcessor.ModelChanged += OnModelChanged;
                    Logger.LogInformation($"{nameof(PostProcessor.ModelChanged)} subscribed");
                }
                return base.OnInitializedAsync();
            }
        
            /// <inheritdoc />
            public ValueTask DisposeAsync()
            {
                if (PostProcessor != null)
                {
                    PostProcessor.ModelChanged -= OnModelChanged;
                    Logger.LogInformation($"{nameof(PostProcessor.ModelChanged)} unsubsribed");
                }
                return new ValueTask(Task.CompletedTask);
            }
        
            private void OnModelChanged(object sender, ElementConstructModel model)
            {
                if (Model != model) return;
                _modelPreviewFragment = new RenderFragment(builder =>
                {
                    builder.AddContent(0, model.ToMarkup());
                });
                StateHasChanged();
            }
    }
}