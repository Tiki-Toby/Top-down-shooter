using System;
using System.Collections.Generic;
using Tools;

namespace BuffLogic
{
    public sealed class BuffManager : BaseDisposable
    {
        private readonly Dictionary<Type, IBaseBuffManager> _buffManagers;

        public BuffManager()
        {
            _buffManagers = new Dictionary<Type, IBaseBuffManager>();
            AddDisposableAction(_buffManagers.Clear);
        }

        public void AddBuff<TValue>(IBuffableValue<TValue> targetValue, IBuff<TValue> buff)
        {
            if (!_buffManagers.TryGetValue(typeof(TValue), out IBaseBuffManager buffManager))
            {
                buffManager = AddDisposable(new BaseBuffManager<TValue>());
                _buffManagers.Add(typeof(TValue), buffManager);
            }
            else
            {
                
            }
            
            buffManager.AddBuff(targetValue, buff);
        }

        public void Update()
        {
            foreach (var buffManager in _buffManagers)
            {
                buffManager.Value.Update();
            }
        }
    }
}