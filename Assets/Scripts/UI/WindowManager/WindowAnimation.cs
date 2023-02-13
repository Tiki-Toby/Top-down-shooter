using System;

namespace Game.Ui.WindowManager
{
    public abstract class WindowAnimation
    {
        public abstract void Animate(Window window, Action<Window> onAnimationComplete);
    }
}