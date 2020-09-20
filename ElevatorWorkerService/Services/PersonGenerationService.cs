using System;
using System.Threading;
using System.Threading.Tasks;
using ElevatorWorkerService.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ElevatorWorkerService.Services
{
    public class PersonGenerationService : BackgroundService
    {
        private readonly IBuilding _building;
        private readonly ILogger<PersonGenerationService> _logger;
        private readonly ISettingsService _settingsService;
        private readonly IBackgroundTaskQueue _taskQueue;

        // ReSharper disable once TooManyDependencies
        public PersonGenerationService(ILogger<PersonGenerationService> logger, IBackgroundTaskQueue taskQueue,
            IBuilding building, ISettingsService settingsService)
        {
            _logger = logger;
            _taskQueue = taskQueue;
            _building = building;
            _settingsService = settingsService;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken).ConfigureAwait(false);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken).ConfigureAwait(false);
                try
                {
                    await workItem(stoppingToken).ConfigureAwait(false);
                    _logger.LogInformation(
                        $"After Generation Building Filling Rate: {_building.GetCurrentFillingRate(_settingsService.MaxPersonsPerBuilding)}");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"Error occured executing {nameof(workItem)}");
                }
            }
        }
    }
}