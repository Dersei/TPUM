using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPUM.Logic.Interfaces;

namespace TPUM.Logic
{
    public class StringLogSender : IObservable<string>
    {
        private readonly List<IObserver<string>> _observers;
        private readonly TimeSpan _logInterval;
        private readonly IReportable _userSystem;

        public StringLogSender(IReportable reportable, TimeSpan logInterval)
        {
            _userSystem = reportable;
            _observers = new List<IObserver<string>>();
            _logInterval = logInterval;
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new Cleaner(_observers, observer);
        }

        public async void SendReport()
        {
            while (true)
            {
                string log = _userSystem.CreateReport();
                foreach (IObserver<string> observer in _observers)
                {
                    observer.OnNext(log);
                }

                await Task.Delay(_logInterval);
            }

        }


        private class Cleaner : IDisposable
        {
            private readonly List<IObserver<string>> _observers;
            private readonly IObserver<string> _observer;

            public Cleaner(List<IObserver<string>> observers, IObserver<string> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null) _observers.Remove(_observer);
            }
        }

    }
}
