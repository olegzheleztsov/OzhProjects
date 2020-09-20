using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace ElevatorWorkerService.Models
{
    public class OpenedElevatorState : BaseElevatorState
    {
        public OpenedElevatorState(ISubject<IElevatorState> stateSubject, double endTime)
            : base(stateSubject)
        {
            EndTime = endTime;
        }

        public override ElevatorState State => ElevatorState.Opened;

        public double EndTime { get; }
        public double InnerTimer { get; private set; }

        public override async Task DoTick(TimeSpan interval)
        {
            InnerTimer += interval.TotalSeconds;
            if (InnerTimer >= EndTime) Complete();
            await base.DoTick(interval).ConfigureAwait(false);
        }
    }
}