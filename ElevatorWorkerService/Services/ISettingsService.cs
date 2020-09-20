namespace ElevatorWorkerService.Services
{
    public interface ISettingsService
    {
        float GenerationFillingRate { get; }

        float GenerationCheckInterval { get; }

        int MaxPersonsPerBuilding { get; }

        float IntervalBetweenGenerationSinglePerson { get; }

        int FloorCount { get; }

        void SetGenerationFillingRate(float value);
        void SetGenerationCheckInterval(float value);
        void SetMaxPersonsForBuilding(int maxPersons);
        void SetIntervalBetweenGeneratingSinglePerson(float interval);
    }
}