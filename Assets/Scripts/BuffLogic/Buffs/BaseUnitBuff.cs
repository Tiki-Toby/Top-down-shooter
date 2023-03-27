using Units.UnitLogic;

namespace BuffLogic
{
    public abstract class BaseUnitBuff : IBuff<UnitController>
    {
        protected UnitController BuffableValue;
        private bool _isEndConditionDone;

        public int Priority => int.MaxValue;
        public bool IsEndConditionDone => _isEndConditionDone;

        public BaseUnitBuff()
        {
            BuffableValue = null;
            _isEndConditionDone = false;
        }

        public UnitController ApplyBuff(UnitController value)
        {
            BuffableValue = value;
            return value;
        }

        public UnitController RevokeBuff(UnitController value)
        {
            BuffableValue = null;
            return value;
        }

        public void Update()
        {
            if(BuffableValue == null)
                return;

            UpdateBuff();

            _isEndConditionDone = ConditionCheck();
        }

        public abstract void MergeBuffs<TBuff>(TBuff buff) where TBuff : IBuff<UnitController>;
        protected abstract void UpdateBuff();
        protected abstract bool ConditionCheck();
    }
}