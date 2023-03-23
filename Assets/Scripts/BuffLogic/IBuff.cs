using Tools.PriorityTools;

namespace BuffLogic
{
    public interface IBuff { }
    
    public interface IBuff<T> : IPrioritizatedModule, IBuff
    {
        bool IsAlive { get; }
        T ApplyBuff(T value);
        T RevokeBuff(T value);
        void Update();
    }
}