using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElevatorLib.Dtos;
using ElevatorWorkerService.Hubs;
using ElevatorWorkerService.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ElevatorWorkerService.Services
{
    [SuppressMessage("ReSharper", "TooManyChainedReferences")]
    public class OutputService : BackgroundService
    {
        private readonly IBuilding _building;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly IElevator _elevator;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly IEventStreamService _eventStreamService;
        private readonly List<PersonAction> _history = new List<PersonAction>();
        private readonly IHubContext<ElevatorHub, IElevatorHub> _hub;
        private readonly ILogger<OutputService> _logger;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly CancellationToken _token;
        private readonly CancellationTokenSource _tokenSource;
        private readonly ISettingsService _settingsService;

        // ReSharper disable once TooManyDependencies
        public OutputService(IBuilding building, IElevator elevator,
            ILogger<OutputService> logger,
            IEventStreamService eventStreamService,
            IHubContext<ElevatorHub, IElevatorHub> hub,
            ISettingsService settingsService)
        {
            _building = building;
            _logger = logger;
            _eventStreamService = eventStreamService;
            _hub = hub;
            _elevator = elevator;
            _settingsService = settingsService;

            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;

            _building.SubscribeToPersonActions(personAction =>
            {
                _history.Add(personAction);
                if (_history.Count > 100)
                {
                    _history.RemoveAt(0);
                }
            }, _token);
            _eventStreamService.SubscribeToGeneration(genAction =>
            {
                if (genAction == "FINISHED")
                {
                    _logger.LogInformation($"History count: {_history.Count}");
                }
            }, _token);
            _elevator.SubscribeToElevatorActions(OnElevatorAction);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken).ConfigureAwait(false);
            }

            if (stoppingToken.IsCancellationRequested)
            {
                _tokenSource.Cancel();
            }
        }

        private async Task OnElevatorAction(ElevatorAction elevatorAction)
        {
            await _hub.Clients.All.SendBuildingState(GetSnapshot(elevatorAction.Action.ToString()))
                .ConfigureAwait(false);
            await _hub.Clients.All.SendSettings(new SettingsDto
            {
                GenerationCheckInterval = _settingsService.GenerationCheckInterval,
                GenerationFillingRate = _settingsService.GenerationFillingRate,
                IntervalBetweenGenerationSinglePerson = _settingsService.IntervalBetweenGenerationSinglePerson,
                MaxPersonsPerBuilding = _settingsService.MaxPersonsPerBuilding
            }).ConfigureAwait(false);

            _logger.LogInformation($"action: {elevatorAction.Action}");
        }

        // ReSharper disable once TooManyDeclarations
        private BuildingActionSnaphotDto GetSnapshot(string action)
        {
            var floors = _building.Floors.Select(floor => new FloorStateDto
                {FloorNumber = floor.Value.FloorNumber, PersonCount = floor.Value.PersonCount}).ToList();

            var elevatorStateDto = new ElevatorStateDto
            {
                CurrentFloor = _building.Elevator.CurrentFloor,
                PersonCount = _building.Elevator.Persons.Count,
                State = _building.Elevator.State.ToString(),
                TargetFloor = _building.Elevator.TargetFloor
            };

            var nonExitedPersonCount = floors.Sum(floor => floor.PersonCount);
            var exitedCount = _building.ExitedPersons.Count;

            return new BuildingActionSnaphotDto
            {
                Action = action,
                Building = new BuildingStateDto
                {
                    Elevator = elevatorStateDto,
                    Floors = floors.ToArray(),
                    PersonExitedCount = exitedCount,
                    PersonNonExitedCount = nonExitedPersonCount
                }
            };
        }
    }
}