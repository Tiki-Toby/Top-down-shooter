using System;

namespace Game.Ui.WindowManager
{
    public class WindowOpenRequest
    {
        public readonly Type windowType;
        public readonly bool openSynchronously;
        public readonly WindowArguments arguments;
        public readonly Func<bool> allowOpenCallback;

        public WindowOpenRequest(Type windowType, bool openSynchronously, WindowArguments arguments, 
            Func<bool> allowOpenCallback)
        {
            this.windowType = windowType;
            this.openSynchronously = openSynchronously;
            this.arguments = arguments;
            this.allowOpenCallback = allowOpenCallback;
        }
    }
}
