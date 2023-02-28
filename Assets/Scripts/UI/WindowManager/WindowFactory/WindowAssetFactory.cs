using System;
using AssetData;
using UnityEngine;

namespace Game.Ui.WindowManager.WindowFactory
{
    public class WindowAssetFactory : IWindowFactory
    {
        private readonly IGameAssetData _gameAssetData;

        public WindowAssetFactory(IGameAssetData gameAssetData)
        {
            _gameAssetData = gameAssetData;
        }

        public Window Create(Type windowType, Transform windowRoot)
        {
            string windowName = windowType.Name;
            var windowObjectRef = _gameAssetData.GetUiViewObjectReferenceById(windowName);

            if (windowObjectRef == null)
            {
                Debug.LogWarning("Warning! No prefab found in resources");
                return null;
            }

            var window = GameObject.Instantiate<Window>(windowObjectRef, windowRoot);
            return window;
        }
    }
}