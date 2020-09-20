using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorWorkerService.Models
{
    /// <summary>
    ///     Implemented by any elevator
    /// </summary>
    public interface IElevator
    {
        /// <summary>
        ///     Elevator state name
        /// </summary>
        ElevatorState State { get; }

        /// <summary>
        ///     People inside elevator
        /// </summary>
        IList<IPerson> Persons { get; }

        /// <summary>
        ///     Elevator current floor
        /// </summary>
        int CurrentFloor { get; }

        /// <summary>
        ///     Elevator's target floor
        /// </summary>
        int TargetFloor { get; }

        /// <summary>
        ///     Check whether elevator is empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        ///     Returns elevator free space
        /// </summary>
        int FreeSpace { get; }

        /// <summary>
        ///     Put person to elevator
        /// </summary>
        /// <param name="person">Person to put into elevator</param>
        /// <returns>True if put succeed</returns>
        bool TryAttachToElevator(IPerson person);

        /// <summary>
        ///     Check the person in elevator
        /// </summary>
        /// <param name="person">Person to check</param>
        /// <returns>True if the person in elevator</returns>
        bool Contains(IPerson person);

        /// <summary>
        ///     Remove person from elevator
        /// </summary>
        /// <param name="person">Person to remove from elevator</param>
        void Drop(IPerson person);

        /// <summary>
        ///     Updates elevator state
        /// </summary>
        /// <param name="interval">Interval between updates</param>
        /// <returns></returns>
        Task DoTick(TimeSpan interval);

        /// <summary>
        ///     Set elevator current floor
        /// </summary>
        /// <param name="floor"></param>
        void SetCurrentFloor(int floor);

        /// <summary>
        ///     Subscribes to state exited event
        /// </summary>
        /// <param name="action">Subscriber</param>
        /// <param name="cancellationToken">Unsubscribe token</param>
        void SubscribeToElevatorCompletion(Action<IElevatorState> action, CancellationToken cancellationToken);

        /// <summary>
        ///     Subscribes to elevator action events
        /// </summary>
        /// <param name="action">Subscriber</param>
        /// <param name="cancellationToken">Unsubscribe token</param>
        void SubscribeToElevatorActions(Func<ElevatorAction, Task> action);

        /// <summary>
        ///     Start elevator moving
        /// </summary>
        /// <param name="targetFloor">Target elevator floor</param>
        void Move(int targetFloor);

        /// <summary>
        ///     Opens elevator door
        /// </summary>
        void Open();

        /// <summary>
        ///     Enter opened state again (wait people)
        /// </summary>
        void RefreshOpen();
    }
}