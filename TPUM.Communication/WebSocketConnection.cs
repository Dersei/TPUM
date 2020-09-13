using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TPUM.Communication
{
    public abstract class WebSocketConnection : IObservable<bool>
    {
        public virtual Action<string> OnMessage { set; protected get; } = x => { };
        public virtual Action OnClose { set; protected get; } = () => { };
        public virtual Action OnError { set; protected get; } = () => { };

        public readonly List<IObserver<bool>> Observers = new List<IObserver<bool>>();
        public abstract bool IsClosed { get; }

        public async Task SendAsync(string message)
        {
            await SendTask(message);
        }

        public abstract Task DisconnectAsync();

        protected abstract Task SendTask(string message);

        public IDisposable? Subscribe(IObserver<bool> observer)
        {
            Observers.Add(observer);
            return null;
        }
    }
}