using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace ElevatorWorkerService.Models
{
    public class MovingElevatorState : BaseElevatorState
    {
        private double innerTimer;

        public MovingElevatorState(ISubject<IElevatorState> stateSubject, double endTime)
            : base(stateSubject)
        {
            EndTime = endTime;
        }

        public override ElevatorState State => ElevatorState.Moving;

        private double EndTime { get; }

        public override async Task DoTick(TimeSpan interval)
        {
            innerTimer += interval.TotalSeconds;
            if (innerTimer >= EndTime)
            {
                Complete();
            }

            await base.DoTick(interval).ConfigureAwait(false);
        }
    }
}