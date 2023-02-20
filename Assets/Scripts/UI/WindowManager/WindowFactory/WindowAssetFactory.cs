using System;
using AssetData;
using GameFlow.Client.Infrastructure;
using UnityEngine;

namespace Game.Ui.WindowManager.WindowFactory
{
    public class WindowAssetFactory : IWindowFactory
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly IAssetInstantiator _assetInstantiator;

        public WindowAssetFactory(IGameAssetData gameAssetData,
            IAssetInstantiator assetInstantiator)
        {
            _gameAssetData = gameAssetData;
            _assetInstantiator = assetInstantiator;
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

            var window = _assetInstantiator.Instantiate<Window>(windowObjectRef, windowRoot);
            return window;
        }
    }
}