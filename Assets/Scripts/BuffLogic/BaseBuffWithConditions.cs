using UnityEngine;

namespace BuffLogic
{
    public abstract class BaseBuffWithConditions<T>: IBuff<T>
    {
        private readonly ABuffConditionsCollection _buffConditionsCollection;

        private int _priority;
        
        public int Priority => _priority;
        public bool IsAlive => !_buffConditionsCollection.IsEndConditionCorrect;

        protected BaseBuffWithConditions(EBuffConditionCollectionType buffConditionCollectionType, int priority = int.MaxValue)
        {
            _buffConditionsCollection = CreateBuffConditionsCollection(buffConditionCollectionType);
            _priority = priority;
        }

        public abstract T ApplyBuff(T value);
        public abstract T RevokeBuff(T value);
        
        public void Update()
        {
            _buffConditionsCollection.Update();
        }

        public void ForceSetPriority(int priority)
        {
            _priority = priority;
        }
        
        public void AddCondition(IBuffCondition condition)
        {
            _buffConditionsCollection.AddCondition(condition);
        }

        public ABuffConditionsCollection CreateBuffConditionsCollection(EBuffConditionCollectionType buffConditionCollectionType)
        {
            switch (buffConditionCollectionType)
            {
                case  EBuffConditionCollectionType.Once:
                    return new BuffConditionOnceCollection();
                case EBuffConditionCollectionType.Complex:
                    return new BuffConditionComplexCollection();
                
                default:
                    Debug.LogError("Unknown type of BuffConditionsCollection");
                    return new BuffConditionOnceCollection();
            }
        }
    }
}