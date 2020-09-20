using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElevatorWorkerService.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ozh.Utility;
using Ozh.Utility.Collections;

namespace ElevatorWorkerService.Services
{
    public class GenerationPersonChecker : BackgroundService
    {
        private readonly ISettingsService _settingsService;
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IBuilding _building;
        private readonly ILogger<GenerationPersonChecker> _logger;

        // ReSharper disable once TooManyDependencies
        public GenerationPersonChecker(ISettingsService settingsService, IBackgroundTaskQueue taskQueue, IBuilding building, ILogger<GenerationPersonChecker> logger)
        {
            _settingsService = settingsService;
            _taskQueue = taskQueue;
            _building = building;
            _logger = logger;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(_settingsService.GenerationCheckInterval), stoppingToken).ConfigureAwait(false);
                if (IsShouldGeneratePersons && MaxPersonsToGenerate > 0)
                {
                    var personsToGenerate = RandomUtility.InRange(1, MaxPersonsToGenerate + 1);
                    _taskQueue.QueueBackgroundWorkItem(async token =>
                    {
                        if (!token.IsCancellationRequested)
                        {
                            await OnGenerateBunchOfPersons(token, personsToGenerate).ConfigureAwait(false);
                        }
                    });
                }
                else
                {
                    _logger.LogInformation($"Generation is not required, {nameof(IsShouldGeneratePersons)}: {IsShouldGeneratePersons}, {nameof(MaxPersonsToGenerate)}: {MaxPersonsToGenerate}");
                }
            }
        }

        private IPerson GeneratePerson(IFloor floor)
        {
            var otherFloors = _building.GetFloorsExcept(floor);
            var otherFloorList = otherFloors.ToList();
            if (!otherFloorList.Any())
            {
                throw new InvalidOperationException(nameof(otherFloors));
            }

            var targetFloor = otherFloorList.GetRandomElement();
            return new Person(floor.FloorNumber, targetFloor.FloorNumber);
        }

        private async Task OnGenerateBunchOfPersons(CancellationToken cancellationToken, int personCount)
        {
            await Observable.Interval(TimeSpan.FromSeconds(_settingsService.IntervalBetweenGenerationSinglePerson))
                .Take(personCount).Do(_ =>
                {
                    var freeFloors = _building.GetNotFullFloors();
                    var generationFloor = freeFloors.GetRandomElement();
                    if (generationFloor == null)
                    {
                        return;
                    }

                    var person = GeneratePerson(generationFloor);
                    if (!_building.TryEnterOnFloorToWaitElevator(person, generationFloor))
                    {
                        //some error handling
                    }
                });
        }

        private float GetCurrentFillingRate()
            => _building.GetCurrentFillingRate(_settingsService.MaxPersonsPerBuilding);

        private bool IsShouldGeneratePersons
            => GetCurrentFillingRate() < _settingsService.GenerationFillingRate;

        private int MaxPersonsToGenerate
            => MathUtils.Clamp(_settingsService.MaxPersonsPerBuilding - _building.CurrentPersonsInBuilding, 0,
                _settingsService.MaxPersonsPerBuilding);
    }
}
