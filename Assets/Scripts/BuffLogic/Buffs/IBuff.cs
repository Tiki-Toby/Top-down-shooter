using Tools.PriorityTools;

namespace BuffLogic
{
    public interface IBuff<T> : IPrioritizatedModule
    {
        void ApplyBuff(out T value);
        void RevokeBuff(out T value);
        bool EndConditionExec();
    }
}