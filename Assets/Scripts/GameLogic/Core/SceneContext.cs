using System;
using AssetData;
using Game.Ui.WindowManager;
using Game.Ui.WindowManager.WindowFactory;
using GameLogic.UnitLogic;
using UnityEngine;

namespace GameLogic.Core
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private GameAssetsDataHolder _gameAssetsDataHolder;
        [SerializeField] private WindowsManager _windowsManager;

        private UnitManager _unitManager;

        private void Awake()
        {
            var windowFactory = new WindowAssetFactory(_gameAssetsDataHolder);
            _windowsManager.SetWindowFactory(windowFactory);

            _unitManager = new UnitManager(_gameAssetsDataHolder);
        }

        private void Update()
        {
            _unitManager.Update();
        }
    }
}