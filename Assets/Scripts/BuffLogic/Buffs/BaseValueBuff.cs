using System;
using System.Collections.Generic;

namespace BuffLogic
{
    public abstract class BaseValueBuff<T> : IBuff<T>
    {
        private readonly List<Func<bool>> _conditions;
        protected readonly float BuffValue;
        public int Priority => 2;

        protected BaseValueBuff(float buffValue)
        {
            _conditions = new List<Func<bool>>();
            BuffValue = buffValue;
        }

        public abstract void ApplyBuff(out T value);

        public abstract void RevokeBuff(out T value);
        
        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public bool EndConditionExec()
        {
            foreach (var condition in _conditions)
            {
                if (condition.Invoke())
                    return true;
            }

            return false;
        }
    }
}