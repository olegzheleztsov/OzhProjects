using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ozh.Utility.Collections;

namespace ElevatorWorkerService.Models
{
    /// <summary>
    ///     Represents elevator at building that transports people between floors
    /// </summary>
    public class Elevator : IElevator
    {
        private readonly Subject<IElevatorState> _elevatorStateObservable;
        private readonly Subject<ElevatorAction> _elevatorActionObservable;

        private readonly ILogger<Elevator> _logger;

        private IElevatorState _elevatorState;

        /// <summary>
        ///     Creates elevator instance
        /// </summary>
        /// <param name="logger">Logger for output debug info</param>
        public Elevator(ILogger<Elevator> logger)
        {
            _logger = logger;
            _elevatorStateObservable = new Subject<IElevatorState>();
            _elevatorActionObservable = new Subject<ElevatorAction>();
            _elevatorState = ElevatorStateFactory.CreateNone(_elevatorStateObservable);
            CurrentFloor = TargetFloor = 0;
        }

        private int MaxPersons { get; } = 4;

        private double FloorHeight { get; } = 2;

        private double LoadUnloadTime { get; } = 3;

        private int PersonCount => Persons.Count;

        private bool IsFull => PersonCount >= MaxPersons;

        /// <summary>
        ///     Current state name of the elevator
        /// </summary>
        public ElevatorState State => _elevatorState?.State ?? ElevatorState.None;

        /// <summary>
        ///     Persons in the elevator
        /// </summary>
        public IList<IPerson> Persons { get; } = new List<IPerson>();

        /// <summary>
        ///     Floor where elevator is
        /// </summary>
        public int CurrentFloor { get; private set; }

        /// <summary>
        ///     Target elevator floor
        /// </summary>
        public int TargetFloor { get; private set; }

        /// <summary>
        ///     Elevator moving speed ( meters per sec )
        /// </summary>
        public double Speed => 1;

        /// <summary>
        ///     Is any person in elevator ?
        /// </summary>
        public bool IsEmpty => Persons.Count == 0;

        /// <summary>
        ///     Free space in the elevator - number of persons allowed to come in elevator
        /// </summary>
        public int FreeSpace => MathUtils.Clamp(MaxPersons - PersonCount, 0, MaxPersons);

        /// <summary>
        ///     Check that person inside elevator
        /// </summary>
        /// <param name="person">Person to check</param>
        /// <returns>True if person inside elevator</returns>
        public bool Contains(IPerson person)
        {
            return Persons.Contains(person);
        }

        /// <summary>
        ///     Try add person to elevator
        /// </summary>
        /// <param name="person">Person to add</param>
        /// <returns>True if person successfully added, False - when elevator is full or person invariant check failed</returns>
        public bool TryAttachToElevator(IPerson person)
        {
            if (IsFull)
            {
                return false;
            }

            if (person.CurrentFloor != CurrentFloor)
            {
                return false;
            }

            Persons.Add(person);
            return true;
        }

        /// <summary>
        ///     Remove person from elevator
        /// </summary>
        /// <param name="person">Person to remove</param>
        public void Drop(IPerson person)
        {
            Persons.Remove(person);
        }

        /// <summary>
        ///     Updates elevator state
        /// </summary>
        /// <param name="interval">Interval between current and previous update</param>
        /// <returns></returns>
        public async Task DoTick(TimeSpan interval)
        {
            await _elevatorState.DoTick(interval).ConfigureAwait(false);
        }

        /// <summary>
        ///     Set elevator current floor. Also set this floor for people inside elevator
        /// </summary>
        /// <param name="floor">Floor to set in elevator</param>
        public void SetCurrentFloor(int floor)
        {
            CurrentFloor = floor;
            foreach (var person in Persons)
            {
                person.SetFloor(floor);
            }
        }

        /// <summary>
        ///     Add subscriber to elevator state finishing event
        /// </summary>
        /// <param name="action">Action when event occured</param>
        /// <param name="cancellationToken">Cancellation token to unsubscribe from the events</param>
        public void SubscribeToElevatorCompletion(Action<IElevatorState> action, CancellationToken cancellationToken)
        {
            _elevatorStateObservable.Subscribe(action, cancellationToken);
        }

        /// <inheritdoc />
        public void SubscribeToElevatorActions(Func<ElevatorAction, Task> action)
        {
            _elevatorActionObservable.SubscribeAsync(action);
        }

        /// <summary>
        ///     Starts move elevator between floors
        /// </summary>
        /// <param name="targetFloor">Target elevator floor</param>
        public void Move(int targetFloor)
        {
            TargetFloor = targetFloor;
            if (CurrentFloor == TargetFloor) return;
            var heightBetweenFloors = Math.Abs(TargetFloor - CurrentFloor) * FloorHeight;
            var moveTime = heightBetweenFloors / Speed;
            SetState(ElevatorStateFactory.CreateMoved(_elevatorStateObservable, moveTime));
        }

        /// <summary>
        ///     Opens elevator door
        /// </summary>
        public void Open()
        {
            if (CurrentFloor == TargetFloor)
            {
                SetState(ElevatorStateFactory.CreateOpened(_elevatorStateObservable, LoadUnloadTime));
            }
        }

        /// <summary>
        ///     Normally we cannot enter the same state two or more times. In this case we break this rule. We force opened state
        ///     again even before already was opened state
        /// </summary>
        public void RefreshOpen()
        {
            SetState(ElevatorStateFactory.CreateOpened(_elevatorStateObservable, LoadUnloadTime), true);
        }

        /// <summary>
        ///     Set the new state on elevator
        /// </summary>
        /// <param name="newState">New state</param>
        /// <param name="force">
        ///     Normally we don't allow enter the same state two times, but this flag force enter the same state
        ///     again
        /// </param>
        // ReSharper disable once FlagArgument
        private void SetState(IElevatorState newState, bool force = false)
        {
            if (_elevatorState.State == newState.State && !force) return;
            var previousState = _elevatorState;
            _elevatorState = newState;
            UpdateElevatorActions(previousState, newState);
        }

        private void UpdateElevatorActions(IElevatorState previousState, IElevatorState newState)
        {
            switch (newState.State)
            {
                case ElevatorState.Moving when previousState.State != ElevatorState.Moving:
                    _elevatorActionObservable.OnNext(new ElevatorAction(ElevatorActionType.StartMoving));
                    break;
                case ElevatorState.Opened when previousState.State == ElevatorState.Moving:
                    _elevatorActionObservable.OnNext(new ElevatorAction(ElevatorActionType.EndMoving));
                    _elevatorActionObservable.OnNext(new ElevatorAction(ElevatorActionType.Opening));
                    break;
                case ElevatorState.None:
                    break;
                default:
                    _elevatorActionObservable.OnNext(new ElevatorAction(ElevatorActionType.RetryOpening));
                    break;
            }
        }
    }
}