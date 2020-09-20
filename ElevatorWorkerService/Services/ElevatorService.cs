using ElevatorWorkerService.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorWorkerService.Services
{
    public class ElevatorService : BackgroundService
    {
        private readonly IElevator elevator;
        private readonly IBuilding building;
        private readonly TimeSpan tickInterval = TimeSpan.FromSeconds(0.5);
        private readonly ILogger<ElevatorService> logger;

        private DateTime? lastTickTime = null;
        private double totalTimer = 0.0;

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken cancellationToken;

        public ElevatorService(IBuilding building, IElevator elevator, ILogger<ElevatorService> logger)
        {
            this.building = building;
            this.elevator = elevator;
            this.logger = logger;
            this.cancellationToken = this.cancellationTokenSource.Token;
            this.elevator.SubscribeToElevatorCompletion(OnElevatorStateReadyToChange, cancellationToken);

            if(this.elevator.State == ElevatorState.None)
            {
                this.elevator.Open();
            }
        }

        private void OnElevatorStateReadyToChange(IElevatorState state)
        {
            if(state.State == ElevatorState.Opened)
            {
                OnElevatorIsTryingToMove();
            } else if(state.State == ElevatorState.Moving)
            {
                OnElevatorIsOpening();
            }
        }

        private void OnElevatorIsOpening()
        {
            elevator.SetCurrentFloor(elevator.TargetFloor);
            elevator.Open();
            building.UnloadPersonsFromElevatorToFloor();
            building.LoadNewPersonsFromFloorToElevator();
        }

        private void OnElevatorIsTryingToMove()
        {
            var targetFloor = building.SelectElevatorTargetFloor();
            if(targetFloor == null)
            {
                elevator.RefreshOpen();
            } else
            {
                elevator.Move(targetFloor.FloorNumber);
            }
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Observable.Interval(tickInterval).Do(async _ => {
                var now = DateTime.UtcNow;
                if(lastTickTime == null)
                {
                    lastTickTime = now;
                }
                double diffSeconds = (now - lastTickTime.Value).TotalSeconds;
                lastTickTime = now;
                totalTimer += diffSeconds;
                await DoTick(TimeSpan.FromSeconds(diffSeconds)).ConfigureAwait(false);
            });
        }

        private async Task DoTick(TimeSpan interval)
        {
            await elevator.DoTick(interval).ConfigureAwait(false);
        }
    }
}
