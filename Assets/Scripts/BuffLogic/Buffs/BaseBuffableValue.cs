using System;
using Tools.PriorityTools;

namespace BuffLogic
{
    // public abstract class BaseBuffableValue<TObserver, TValue> : IDisposable where TObserver : IObservable<TValue>, new()
    public abstract class BaseBuffableValue<TValue> : IDisposable
    {
        private readonly PrioritizeLinkedList<IBuff<TValue>> _buffs;

        public BaseBuffableValue()
        {
            _buffs = new PrioritizeLinkedList<IBuff<TValue>>();
        }

        protected abstract void ApplyBuffs(IBuff<TValue> initiatorBuff);

        public void AddBuff(IBuff<TValue> buff)
        {
            _buffs.Add(buff);
            ApplyBuffs(buff);
        }

        public void RemoveBuff(IBuff<TValue> buff)
        {
            _buffs.Remove(buff);
            ApplyBuffs(buff);
        }

        public void Dispose()
        {
            _buffs.Dispose();
        }
    }
}