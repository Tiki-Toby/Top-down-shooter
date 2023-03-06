using Tools.PriorityTools;

namespace BuffLogic
{
    public interface IBuff { }
    
    public interface IBuff<T> : IPrioritizatedModule, IBuff
    {
        T ApplyBuff(T value);
        T RevokeBuff(T value);
        bool EndConditionExec();
    }
}