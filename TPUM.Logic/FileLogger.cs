using System;
using System.IO;

namespace TPUM.Logic
{
    public class FileLogger : IObserver<string>
    {
        private IDisposable? _cleaner;

        public virtual void Subscribe(IObservable<string> provider)
        {
            _cleaner = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            _cleaner?.Dispose();
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(string value)
        {
            File.AppendAllText("userreport.txt", value);
            Console.WriteLine(value);
        }
    }
}