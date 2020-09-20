using System.Threading.Tasks;
using ElevatorLib.Dtos;
using ElevatorWorkerService.Services;
using Microsoft.AspNetCore.SignalR;

namespace ElevatorWorkerService.Hubs
{
    public class ElevatorHub : Hub<IElevatorHub>
    {
        private readonly ISettingsService _settingsService;
        private readonly IEventStreamService _eventStreamService;

        public ElevatorHub(ISettingsService settingsService, IEventStreamService eventStreamService)
        {
            _settingsService = settingsService;
            _eventStreamService = eventStreamService;
        }

        

        public async Task SendBuildingState(BuildingActionSnaphotDto buildingSnapshot)
        {
            await Clients.All.SendBuildingState(buildingSnapshot).ConfigureAwait(false);
        }

        public async Task SendSettings(SettingsDto settingsDto)
        {
            await Clients.All.SendSettings(settingsDto).ConfigureAwait(false);
        }

        private void NotifyChange(string parameterName, object parameterValue)
        {
            _eventStreamService?.ControlParameterObservable.OnNext(new ControlParameter
            {
                ParameterName = parameterName,
                ParameterValue = parameterValue
            });
        }

        public async Task SetGenerationFillingRate(float value)
        {
            _settingsService.SetGenerationFillingRate(value);
            await SendSettings(GetSettingsDto()).ConfigureAwait(false);
            NotifyChange(nameof(_settingsService.GenerationFillingRate), value);
        }

        public async Task SetGenerationCheckInterval(float value)
        {
            _settingsService.SetGenerationCheckInterval(value);
            await SendSettings(GetSettingsDto()).ConfigureAwait(false);
            NotifyChange(nameof(_settingsService.GenerationCheckInterval), value);
        }

        public async Task SetMaxPersonsForBuilding(int maxPersons)
        {
            _settingsService.SetMaxPersonsForBuilding(maxPersons);
            await SendSettings(GetSettingsDto()).ConfigureAwait(false);
            NotifyChange(nameof(_settingsService.MaxPersonsPerBuilding), maxPersons);
        }

        public async Task SetIntervalBetweenGeneratingSinglePerson(float interval)
        {
            _settingsService.SetIntervalBetweenGeneratingSinglePerson(interval);
            await SendSettings(GetSettingsDto()).ConfigureAwait(false);
            NotifyChange(nameof(_settingsService.IntervalBetweenGenerationSinglePerson), interval);
        }

        private SettingsDto GetSettingsDto()
        {
            return new SettingsDto
            {
                GenerationCheckInterval = _settingsService.GenerationCheckInterval,
                GenerationFillingRate = _settingsService.GenerationFillingRate,
                IntervalBetweenGenerationSinglePerson = _settingsService.IntervalBetweenGenerationSinglePerson,
                MaxPersonsPerBuilding = _settingsService.MaxPersonsPerBuilding
            };
        }
    }
}