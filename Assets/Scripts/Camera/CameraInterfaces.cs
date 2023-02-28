using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HairyEngine.HairyCamera
{
    public interface ICameraComponent
    {
        int PriorityOrder { get; }
    }
    public interface IPreMove : ICameraComponent
    {
        void HandleStartMove(Vector3 position);
    }
    public interface IPositionChanged : ICameraComponent
    {
        Vector3 HandlePositionChange(Vector3 newPosition);
    }
    
    public interface IViewSizeChanged : ICameraComponent
    {
        float HandleSizeChanged(float newSize);
    }
}
