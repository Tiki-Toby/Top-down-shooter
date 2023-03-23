namespace BuffLogic
{
    public abstract class BaseValueBuff<T> : BaseBuffWithConditions<T>
    {
        protected readonly float BuffValue;

        protected BaseValueBuff(float buffValue, 
            EBuffConditionCollectionType buffConditionCollectionType = EBuffConditionCollectionType.Once,
            int priority = int.MaxValue) : base(buffConditionCollectionType, priority)
        {
            BuffValue = buffValue;
        }
    }
}