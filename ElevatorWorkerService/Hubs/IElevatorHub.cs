using System.Threading.Tasks;
using ElevatorLib.Dtos;

namespace ElevatorWorkerService.Hubs
{
    public interface IElevatorHub
    {
        Task SendBuildingState(BuildingActionSnaphotDto buildingSnapshot);

        Task SendSettings(SettingsDto settingsDto);

        Task SetGenerationFillingRate(float value);
        Task SetGenerationCheckInterval(float value);
        Task SetMaxPersonsForBuilding(int maxPersons);
        Task SetIntervalBetweenGeneratingSinglePerson(float interval);
    }
}