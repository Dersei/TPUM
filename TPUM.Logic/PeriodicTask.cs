using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace TPUM.Logic
{
    public class PeriodicTask<T> where T : class
    {
        public PeriodicTask(TimeSpan period)
        {
            Period = period;
        }

        public TimeSpan Period { get; }

        public async IAsyncEnumerable<T> Start(Func<T> function, [EnumeratorCancellation] CancellationToken token = default, Func<T>? cancellationFunc = default)
        {
            while (true)
            {
                await Task.Delay(Period);
                if (token.IsCancellationRequested)
                {
                    yield return cancellationFunc?.Invoke()!;
                    yield break;
                }
                yield return function();
            }
        }
    }
}