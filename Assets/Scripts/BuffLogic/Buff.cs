namespace BuffLogic
{
    public abstract class Buff<T>
    {
        protected T BuffValue;
        protected float AddLessParameter;
        protected float MultDivideParameter;
        
        public abstract T Result { get; }
        
        public Buff()
        {
            BuffValue = default;
            AddLessParameter = 0f;
            MultDivideParameter = 1f;
        }

        public void AddSumBuff(float parameter) => AddLessParameter += parameter;
        public void AddFactorBuff(float factor) => MultDivideParameter *= factor;

        public void AddBuff(float parameter, float factor)
        {
            AddSumBuff(parameter);
            AddFactorBuff(factor);
        }

        public void RevokeBuff(float parameter, float factor)
        {
            AddSumBuff(-parameter);
            AddFactorBuff(1f / factor);
        }
    }
    
    public class FloatBuff : Buff<float>
    {
        public override float Result => BuffValue * MultDivideParameter + AddLessParameter;
    }
}