using System;
using UnityEngine;

namespace Game.Ui.WindowManager
{
    public interface IWindowFactory
    {
        public Window Create(Type windowType, Transform windowRoot);
    }
}