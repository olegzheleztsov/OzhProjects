using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ElevatorClient.Components
{
    public partial class Alert : ComponentBase
    {
        private RenderFragment AlertContent { get; set; } = DefaultAlertContent;
        private bool Visible { get; set; } = false;

        public async Task ShowAsync(TimeSpan interval, RenderFragment content)
        {
            await InvokeAsync(async () =>
            {
                Show(content);
                StateHasChanged();

                await Task.Delay(interval).ConfigureAwait(false);
                Visible = false;
                StateHasChanged();
            }).ConfigureAwait(false);
        }

        public void Show(RenderFragment content)
        {
            InvokeAsync(() =>
            {
                SetAlertMessage(content);
                if (!Visible)
                {
                    Visible = true;
                }
                StateHasChanged();
            });
        }

        public void Hide()
        {
            InvokeAsync(() => {
                if (Visible)
                {
                    Visible = false;
                    StateHasChanged();
                }
            });
        }

        private void SetAlertMessage(RenderFragment content)
        {
            InvokeAsync(() => {
                AlertContent = content;
                StateHasChanged();
            });
        }

        private void OnCloseClick()
        {
            Hide();
        }
    }
}
