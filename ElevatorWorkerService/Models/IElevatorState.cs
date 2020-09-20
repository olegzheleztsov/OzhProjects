using System;
using System.Threading.Tasks;

namespace ElevatorWorkerService.Models
{
    public interface IElevatorState
    {
        ElevatorState State { get; }
        Task DoTick(TimeSpan interval);
    }
}
