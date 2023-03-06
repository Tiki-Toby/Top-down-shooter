namespace BuffLogic
{
    public class AddFloatValueBuff : BaseValueBuff<float>
    {
        public AddFloatValueBuff(float buffValue, float time = -1) : base(buffValue, time, 1)
        {
        }

        public override float ApplyBuff(float value) => value + BuffValue;

        public override float RevokeBuff(float value) => value - BuffValue;
    }
    
    public class MultiFloatValueBuff : BaseValueBuff<float>
    {
        //buffValue - percentage
        public MultiFloatValueBuff(float buffValue, float time = -1) : base(1f + buffValue/100, time, 0)
        {
            
        }

        public override float ApplyBuff(float value) => value * BuffValue;

        public override float RevokeBuff(float value) => value / BuffValue;
    }
}