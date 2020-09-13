using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace TPUM.Logic
{
    public class PeriodicTask<T> where T : class
    {
        public PeriodicTask(TimeSpan period, Func<T> function)
        {
            Period = period;
            _function = function;
        }

        public TimeSpan Period { get; }
        private readonly Func<T> _function;

        public async IAsyncEnumerable<T> Start([EnumeratorCancellation] CancellationToken token = default, Func<T>? cancellationFunc = default)
        {
            while (true)
            {
                await Task.Delay(Period);
                if (token.IsCancellationRequested)
                {
                    yield return cancellationFunc?.Invoke()!;
                    yield break;
                }
                yield return _function();
            }
        }
    }
}