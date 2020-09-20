using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ElevatorWorkerService.Models
{
    public interface IBuilding
    {
        IDictionary<int, IFloor> Floors { get; }
        IElevator Elevator { get; }
        IEnumerable<IFloor> GetNotFullFloors();
        IEnumerable<IFloor> GetFloorsExcept(params IFloor[] exceptFloors);

        bool TryEnterOnFloorToWaitElevator(IPerson person, IFloor floor);

        bool TryExitFromElevator(IPerson person);

        void SubscribeToPersonActions(Action<PersonAction> action, CancellationToken cancellationToken);

        IFloor SelectElevatorTargetFloor();

        void LoadNewPersonsFromFloorToElevator();
        void UnloadPersonsFromElevatorToFloor();

        IFloor GetFloor(int level);

        List<IPerson> ExitedPersons { get; }

        int CurrentPersonsInBuilding { get; }

        float GetCurrentFillingRate(int maxPersonsPerBuilding);
    }
}
