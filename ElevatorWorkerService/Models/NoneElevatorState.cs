using System.Reactive.Subjects;

namespace ElevatorWorkerService.Models
{
    public class NoneElevatorState : BaseElevatorState
    {
        public NoneElevatorState(ISubject<IElevatorState> stateSubject)
            : base(stateSubject)
        {
        }

        public override ElevatorState State => ElevatorState.None;
    }
}