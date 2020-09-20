using Microsoft.Extensions.Logging;

namespace ElevatorWorkerService.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ILogger<SettingsService> _logger;

        public SettingsService(ILogger<SettingsService> logger)
        {
            _logger = logger;
        }

        public float GenerationFillingRate { get; private set; } = 0.5f;

        public float GenerationCheckInterval { get; private set; } = 3.0f;

        /// <inheritdoc />
        public int MaxPersonsPerBuilding { get; private set; } = 90;

        /// <inheritdoc />
        public float IntervalBetweenGenerationSinglePerson { get; private set; } = 0.1f;

        public int FloorCount { get; } = 9;

        /// <inheritdoc />
        public void SetGenerationFillingRate(float value)
        {
            GenerationFillingRate = value;
            _logger.LogInformation($"{nameof(GenerationFillingRate)} changed to: {GenerationFillingRate}");
        }

        /// <inheritdoc />
        public void SetGenerationCheckInterval(float value)
        {
            GenerationCheckInterval = value;
            _logger.LogInformation($"{nameof(GenerationCheckInterval)} changed to: {GenerationCheckInterval}");
        }

        /// <inheritdoc />
        public void SetMaxPersonsForBuilding(int maxPersons)
        {
            MaxPersonsPerBuilding = maxPersons;
            _logger.LogInformation($"{nameof(MaxPersonsPerBuilding)} changed to: {MaxPersonsPerBuilding}");
        }

        /// <inheritdoc />
        public void SetIntervalBetweenGeneratingSinglePerson(float interval)
        {
            IntervalBetweenGenerationSinglePerson = interval;
            _logger.LogInformation(
                $"{nameof(IntervalBetweenGenerationSinglePerson)} changed to: {IntervalBetweenGenerationSinglePerson}");
        }
    }
}