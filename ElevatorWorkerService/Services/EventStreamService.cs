using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;

namespace ElevatorWorkerService.Services
{
    public class EventStreamService : IEventStreamService
    {
        private readonly Subject<string> personGenerationObservable = new Subject<string>();

        public Subject<ControlParameter> ControlParameterObservable { get; } = new Subject<ControlParameter>();

        public void SpawnGeneration(string @event)
        {
            personGenerationObservable.OnNext(@event);
        }

        public void SubscribeToGeneration(Action<string> action, CancellationToken cancellationToken)
        {
            personGenerationObservable.Subscribe(action, cancellationToken);
        }
    }

    public class ControlParameter
    {
        public string ParameterName { get; set; }
        public object ParameterValue { get; set; }
    }
}
