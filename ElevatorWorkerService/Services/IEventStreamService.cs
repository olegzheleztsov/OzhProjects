using System;
using System.Reactive.Subjects;
using System.Threading;

namespace ElevatorWorkerService.Services
{
    public interface IEventStreamService
    {
        void SubscribeToGeneration(Action<string> action, CancellationToken cancellationToken);

        void SpawnGeneration(string @event);

        Subject<ControlParameter> ControlParameterObservable { get; }

    }
}
