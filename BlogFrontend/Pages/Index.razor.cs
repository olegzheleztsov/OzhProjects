using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElevatorLib.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;

namespace ElevatorClient.Pages
{
    public partial class Index : ComponentBase, IDisposable
    {
        private HubConnection _hubConnection;
        private readonly List<BuildingActionSnaphotDto> _buildingActionSnapshotDtos = new List<BuildingActionSnaphotDto>();

        private float GenerationFillingRate { get; set; }

        private float GenerationCheckInterval { get; set; }

        private int MaxPersonsPerBuilding { get; set; }

        private float IntervalBetweenGenerationSinglePerson { get; set; }

        private async Task UpdateGenerationFillingRate(MouseEventArgs args)
        {
            await _hubConnection.SendAsync("SetGenerationFillingRate", GenerationFillingRate).ConfigureAwait(false);
        }

        private async Task UpdateGenerationCheckInterval(MouseEventArgs args)
        {
            await _hubConnection.SendAsync("SetGenerationCheckInterval", GenerationCheckInterval).ConfigureAwait(false);
        }

        private async Task UpdateMaxPersonsPerBuilding(MouseEventArgs args)
        {
            await _hubConnection.SendAsync("SetMaxPersonsForBuilding", MaxPersonsPerBuilding).ConfigureAwait(false);
        }

        private async Task UpdateIntervalBetweenGenerationSinglePerson(MouseEventArgs args)
        {
            await _hubConnection
                .SendAsync("SetIntervalBetweenGeneratingSinglePerson", IntervalBetweenGenerationSinglePerson)
                .ConfigureAwait(false);
        }

        private BuildingStateDto LastBuildingSnapshot
            => _buildingActionSnapshotDtos.Count == 0
                ? null
                : _buildingActionSnapshotDtos[^1].Building;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5100/elevator").Build();
            _hubConnection.On<BuildingActionSnaphotDto>("SendBuildingState", OnBuildingStateReceived);
            _hubConnection.On<SettingsDto>("SendSettings", OnSettingsReceived);

            await _hubConnection.StartAsync().ConfigureAwait(false);
        }

        private void OnBuildingStateReceived(BuildingActionSnaphotDto building)
        {
            _buildingActionSnapshotDtos.Add(building);
            if (_buildingActionSnapshotDtos.Count > 100)
            {
                _buildingActionSnapshotDtos.RemoveAt(0);
            }
            StateHasChanged();
        }

        private void OnSettingsReceived(SettingsDto settingsDto)
        {
            GenerationFillingRate = settingsDto.GenerationFillingRate;
            GenerationCheckInterval = settingsDto.GenerationCheckInterval;
            MaxPersonsPerBuilding = settingsDto.MaxPersonsPerBuilding;
            IntervalBetweenGenerationSinglePerson = settingsDto.IntervalBetweenGenerationSinglePerson;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _ = _hubConnection?.DisposeAsync();
        }
    }
}
