using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorWorkerService.Models
{
    public interface IFloor
    {
        bool IsFull { get; }

        bool IsEmpty { get; }

        int FloorNumber { get; }

        int PersonCount { get; }

        int MaxPersons { get; set; }

        bool TryAttachToFloor(IPerson person);

        IEnumerable<IPerson> DetachPersons(int count);
    }
}
