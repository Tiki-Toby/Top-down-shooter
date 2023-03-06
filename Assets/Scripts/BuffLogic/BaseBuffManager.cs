using System.Collections.Generic;
using Tools;

namespace BuffLogic
{
    public interface IBaseBuffManager
    {
        void AddBuff<T>(IBuffableValue<T> targetValue, IBuff<T> buff);
        void Update();
    }
    
    public class BaseBuffManager<TValue> : BaseDisposable, IBaseBuffManager
    {
        private readonly Dictionary<IBuffableValue<TValue>, PrioritizedBuffLinkedList<TValue>> _buffs;
        private readonly List<IBuffableValue<TValue>> _removableBuffableValues;

        public BaseBuffManager()
        {
            _buffs = new Dictionary<IBuffableValue<TValue>, PrioritizedBuffLinkedList<TValue>>();
            _removableBuffableValues = new List<IBuffableValue<TValue>>();
        }

        public void AddBuff(IBuffableValue<TValue> targetValue, IBuff<TValue> buff)
        {
            if (!_buffs.TryGetValue(targetValue, out var buffList))
            {
                buffList = new PrioritizedBuffLinkedList<TValue>(targetValue);
                _buffs.Add(targetValue, buffList);
            }
            
            buffList.AddBuff(buff);
        }

        public void AddBuff<T>(IBuffableValue<T> targetValue, IBuff<T> buff)
        {
            AddBuff((IBuffableValue<TValue>)targetValue, (IBuff<TValue>)buff);
        }

        public void Update()
        {
            foreach (var buffValuePair in _buffs)
            {
                buffValuePair.Value.Update();
                if(!buffValuePair.Value.IsAlive)
                    _removableBuffableValues.Add(buffValuePair.Key);
            }

            foreach (var removableBuffableValue in _removableBuffableValues)
            {
                _buffs.Remove(removableBuffableValue);
            }
            
            _removableBuffableValues.Clear();
        }
    }
}