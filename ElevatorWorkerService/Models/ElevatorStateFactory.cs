using System.Reactive.Subjects;

namespace ElevatorWorkerService.Models
{
    public static class ElevatorStateFactory
    {
        public static IElevatorState CreateOpened(ISubject<IElevatorState> stateSubject, double endTime)
        {
            return new OpenedElevatorState(stateSubject, endTime);
        }

        public static IElevatorState CreateMoved(ISubject<IElevatorState> stateSubject, double endTime)
        {
            return new MovingElevatorState(stateSubject, endTime);
        }

        public static IElevatorState CreateNone(ISubject<IElevatorState> stateSubject)
        {
            return new NoneElevatorState(stateSubject);
        }
    }
}