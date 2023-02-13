namespace Game.Ui.WindowManager
{
    public abstract class WindowArguments
    {
        public enum OpenType
        {
            Default,
            OnTop,
            HighPriority
        }

        public OpenType OpenTypeValue { get; }

        protected WindowArguments(OpenType openType)
        {
            OpenTypeValue = openType;
        }
    }
}
