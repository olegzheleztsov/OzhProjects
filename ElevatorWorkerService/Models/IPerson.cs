using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorWorkerService.Models
{
    public interface IPerson
    {
        string Id { get; }
        PersonState State { get; }
        int CurrentFloor { get; }
        int TargetFloor { get; }
        void SetFloor(int floor);
        void SetState(PersonState state);
    }
}
