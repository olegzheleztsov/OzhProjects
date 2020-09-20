using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace ElevatorWorkerService.Models
{
    public abstract class BaseElevatorState : IElevatorState
    {
        private readonly ISubject<IElevatorState> _stateSubject;

        private bool _isCompleted;

        protected BaseElevatorState(ISubject<IElevatorState> stateSubject)
        {
            _stateSubject = stateSubject;
        }

        public abstract ElevatorState State { get; }

        public virtual Task DoTick(TimeSpan interval)
        {
            return Task.CompletedTask;
        }

        protected void Complete()
        {
            if (_isCompleted) return;
            _isCompleted = true;
            _stateSubject?.OnNext(this);
        }
    }
}