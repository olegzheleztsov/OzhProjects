using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using ElevatorWorkerService.Services;
using Microsoft.Extensions.Logging;
using Ozh.Utility.Collections;

namespace ElevatorWorkerService.Models
{
    public sealed class Building : IBuilding, IDisposable
    {
        private readonly ILogger<Building> _logger;

        private IDisposable _controlParameterSubscription;

        // ReSharper disable once TooManyDependencies
        public Building(IElevator elevator, ILogger<Building> logger, ISettingsService settingsService,
            IEventStreamService eventStreamService)
        {
            _logger = logger;
            Floors = new Dictionary<int, IFloor>();

            for (var i = 0; i < settingsService.FloorCount; i++)
            {
                Floors.Add(i, new Floor(i, settingsService.MaxPersonsPerBuilding / settingsService.FloorCount));
            }

            Elevator = elevator;
            _controlParameterSubscription = eventStreamService.ControlParameterObservable.Subscribe(cp =>
            {
                if (string.Compare(cp.ParameterName, "MaxPersonsPerBuilding", StringComparison.OrdinalIgnoreCase) !=
                    0) return;

                if (!int.TryParse(cp.ParameterValue.ToString(), out var personsPerBuilding)) return;

                var personsPerFloor = personsPerBuilding / settingsService.FloorCount;
                Floors.Values.ToList().ForEach(f => f.MaxPersons = personsPerFloor);
            });
        }

        private Subject<PersonAction> PersonObservable { get; } = new Subject<PersonAction>();

        public IDictionary<int, IFloor> Floors { get; }

        public IElevator Elevator { get; }

        public List<IPerson> ExitedPersons { get; } = new List<IPerson>();

        public IFloor GetFloor(int level)
        {
            return Floors.ContainsKey(level) ? Floors[level] : null;
        }

        public bool TryEnterOnFloorToWaitElevator(IPerson person, IFloor floor)
        {
            if (floor.IsFull)
            {
                return false;
            }

            if (person.State != PersonState.Unknown)
            {
                return false;
            }

            if (floor.TryAttachToFloor(person))
            {
                person.SetState(PersonState.WaitElevator);
                person.SetFloor(floor.FloorNumber);
                PersonObservable.OnNext(new PersonAction(person, floor, PersonActionType.WaitingForElevator));
                return true;
            }

            return false;
        }

        public bool TryExitFromElevator(IPerson person)
        {
            if (!Elevator.Contains(person))
            {
                return false;
            }

            if (Elevator.State != ElevatorState.Opened)
            {
                return false;
            }

            if (Elevator.CurrentFloor != Elevator.TargetFloor)
            {
                return false;
            }

            if (person.CurrentFloor != person.TargetFloor)
            {
                return false;
            }

            if (person.CurrentFloor != Elevator.CurrentFloor)
            {
                return false;
            }

            Elevator.Drop(person);
            person.SetState(PersonState.Exited);
            PersonObservable.OnNext(new PersonAction(person, GetFloor(person.CurrentFloor),
                PersonActionType.ExitedElevator));
            return true;
        }

        public void LoadNewPersonsFromFloorToElevator()
        {
            var floor = GetFloor(Elevator.CurrentFloor);
            if (Elevator.FreeSpace > 0 && floor.PersonCount > 0)
            {
                var count = Math.Min(Elevator.FreeSpace, floor.PersonCount);
                var detachedPersons = floor.DetachPersons(count);
                foreach (var person in detachedPersons)
                {
                    if (!Elevator.TryAttachToElevator(person))
                    {
                        _logger.LogInformation("Impossible to enter in elevator");
                    }
                }
            }
        }

        public void UnloadPersonsFromElevatorToFloor()
        {
            var persons = Elevator.Persons.ToList();
            persons.ForEach(person =>
            {
                if (person.TargetFloor == Elevator.CurrentFloor)
                {
                    if (TryExitFromElevator(person))
                    {
                        _logger.LogInformation($"Person: {person.Id} has exited on the floor: {person.TargetFloor}");
                    }
                }
            });
        }

        public IEnumerable<IFloor> GetNotFullFloors()
        {
            return Floors.Values.Where(floor => !floor.IsFull);
        }

        public IEnumerable<IFloor> GetFloorsExcept(params IFloor[] exceptFloors)
        {
            return Floors.Values.Where(floor => !exceptFloors.Contains(floor));
        }

        public void SubscribeToPersonActions(Action<PersonAction> action, CancellationToken cancellationToken)
        {
            PersonObservable.Subscribe(action, cancellationToken);
        }

        public IFloor SelectElevatorTargetFloor()
        {
            if (!Elevator.IsEmpty)
            {
                return GetFloor(Elevator.Persons.First().TargetFloor);
            }

            var availableFloorsWithPersons =
                GetFloorsExcept(GetFloor(Elevator.CurrentFloor)).Where(floor => !floor.IsEmpty).ToList();
            if (availableFloorsWithPersons.Count > 0)
            {
                return availableFloorsWithPersons.GetRandomElement();
            }

            return null;
        }

        public int CurrentPersonsInBuilding
        {
            get { return Floors.Values.Sum(floor => floor.PersonCount) + Elevator.Persons.Count; }
        }

        public float GetCurrentFillingRate(int maxPersonsPerBuilding)
        {
            return MathUtils.Clamp(
                CurrentPersonsInBuilding / (float) maxPersonsPerBuilding, 0f, 1f);
        }

        public void Dispose()
        {
            _controlParameterSubscription?.Dispose();
            _controlParameterSubscription = null;
        }
    }
}