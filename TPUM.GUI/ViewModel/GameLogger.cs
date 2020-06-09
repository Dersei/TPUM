using System;
using System.IO;
using System.Text;

namespace TPUM.GUI.ViewModel
{
    public class GameLogger : IObserver<string>
    {
        private IDisposable? _cleaner;
        private readonly StringBuilder _sb;

        public GameLogger(StringBuilder sb)
        {
            _sb = sb;
        }

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
            _sb.Append(value);
        }
    }
}