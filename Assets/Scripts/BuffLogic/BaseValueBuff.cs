using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuffLogic
{
    public abstract class BaseValueBuff<T> : IBuff<T>
    {
        private readonly List<Func<bool>> _conditions;
        protected readonly float BuffValue;

        private int _priority;
        
        public int Priority => _priority;

        protected BaseValueBuff(float buffValue, float time = -1, int priority = int.MaxValue)
        {
            _conditions = new List<Func<bool>>();
            BuffValue = buffValue;

            if (time > 0)
            {
                time += Time.time;
                AddCondition(() => time <= Time.time);
            }

            _priority = priority;
        }

        public abstract T ApplyBuff(T value);
        public abstract T RevokeBuff(T value);
        
        public void ForceSetPriority(int priority)
        {
            _priority = priority;
        }
        
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