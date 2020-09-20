using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ElevatorWorkerService
{
    public static class ObservableExtensions
    {
        // ReSharper disable once TooManyDeclarations
        public static IDisposable SubscribeAsync<T>(this IObservable<T> source, Func<T, Task> asyncAction,
            Action<Exception> exceptionHandler = null)
        {
            async Task<Unit> WrappedFunc(T value)
            {
                await asyncAction(value).ConfigureAwait(false);
                return Unit.Default;
            }

            return exceptionHandler == null
                ? source.Select(WrappedFunc).Subscribe(_ => { })
                : source.Select(WrappedFunc).Subscribe(_ => { }, exceptionHandler);
        }
    }
}