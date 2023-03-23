using System;
using UniRx;
using UnityEngine;

namespace BuffLogic
{
    public class TimerBuffCondition : IBuffCondition
    {
        private readonly IReactiveProperty<float> _timerProperty;

        public TimerBuffCondition(float timerDuration)
        {
            _timerProperty = new ReactiveProperty<float>(timerDuration);
        }

        public void Subscribe(Action<float> onChangeAction)
        {
            _timerProperty.Subscribe(onChangeAction);
        }
        
        public bool Invoke()
        {
            _timerProperty.Value -= Time.deltaTime;
            return _timerProperty.Value <= 0f;
        }
    }
}