namespace BuffLogic
{
    public class AddFloatValueBuff : BaseValueBuff<float>
    {
        public AddFloatValueBuff(float buffValue) : base(buffValue)
        {
        }

        public override float ApplyBuff(float value) => value + BuffValue;

        public override float RevokeBuff(float value) => value - BuffValue;
    }
    
    public class MultiFloatValueBuff : BaseValueBuff<float>
    {
        //buffValue - percentage
        public MultiFloatValueBuff(float buffValue) : base(1f + buffValue/100)
        {
        }

        public override float ApplyBuff(float value) => value * BuffValue;

        public override float RevokeBuff(float value) => value / BuffValue;
    }
}