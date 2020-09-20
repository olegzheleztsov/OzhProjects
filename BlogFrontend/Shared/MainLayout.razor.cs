using ElevatorClient.Components;
using ElevatorClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components.Web;

namespace ElevatorClient.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        private const string ROOT_PATH = "/";
        private const string BLOG_CONSTRUCT_PATH = "/blogconstruct";
        
        private Alert AlertObject { get; set; }

        private BaseMatIconButton _menuButton;
        private BaseMatMenu _menu;

        private async Task OnMenu(MouseEventArgs e)
        {
            if (_menu != null && _menuButton != null)
            {
                await _menu.OpenAsync(_menuButton.Ref).ConfigureAwait(false);
            }
        }

        private void NavigateTo(string path)
        {
            NavigationManager.NavigateTo(path);
        }

        [Inject] private IUiElementStorage ElementStorage { get; set; }
        
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                ElementStorage.AddElement<Alert>(AlertObject);
            }
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
