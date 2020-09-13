using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace TPUM.Client.Logic
{
    public class ReactiveService : IDisposable
    {
        public TimeSpan Period { get; private set; }

        public event EventHandler<ReactiveEventArgs>? Tick;

        public ReactiveService(TimeSpan period)
        {
            Period = period;
        }

        public void Start()
        {
            IObservable<long> timerObservable = Observable.Interval(Period);
            _timerSubscription = timerObservable.ObserveOn(Scheduler.Default).Subscribe(RaiseTick);
        }

        public void Stop()
        {
            _timerSubscription?.Dispose();
        }

        private IDisposable? _timerSubscription;

        private void RaiseTick(long counter)
        {
            Tick?.Invoke(this, new ReactiveEventArgs(counter));
        }

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _timerSubscription?.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public class ReactiveEventArgs : EventArgs
        {
            public ReactiveEventArgs(long counter)
            {
                Counter = counter;
            }

            public long Counter { get; private set; }
        }
    }
}
