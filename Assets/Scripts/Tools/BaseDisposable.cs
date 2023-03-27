using System;
using System.Collections.Generic;

namespace Tools
{
    public class BaseDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables;
        private Action _onDispose;

        public BaseDisposable()
        {
            _disposables = new List<IDisposable>();
            _onDispose = null;
        }

        public T AddDisposable<T>(T disposable) where T : IDisposable
        {
            _disposables.Add(disposable);
            return disposable;
        }

        public void AddDisposableAction(Action onDispose)
        {
            _onDispose += onDispose;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
            
            _disposables.Clear();
            
            _onDispose?.Invoke();
            _onDispose = null;
        }
    }
}