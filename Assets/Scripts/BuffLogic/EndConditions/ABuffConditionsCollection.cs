using System.Collections.Generic;

namespace BuffLogic
{
    public abstract class ABuffConditionsCollection
    {        
        protected readonly LinkedList<IBuffCondition> Conditions;
        private bool _isEndConditionCorrect;

        public bool IsEndConditionCorrect => _isEndConditionCorrect;

        protected ABuffConditionsCollection()
        {
            Conditions = new LinkedList<IBuffCondition>();
            _isEndConditionCorrect = true;
        }

        public void AddCondition(IBuffCondition condition)
        {
            Conditions.AddFirst(condition);
        }

        public void Update()
        {
            _isEndConditionCorrect = InvokeEndConditions();
        }

        protected abstract bool InvokeEndConditions();
    }
}