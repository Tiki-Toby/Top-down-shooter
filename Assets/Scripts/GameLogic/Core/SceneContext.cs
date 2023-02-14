using AssetData;
using Game.Ui.WindowManager;
using Game.Ui.WindowManager.WindowFactory;
using GameLogic.UnitLogic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameLogic.Core
{
    public class SceneContext : MonoBehaviour
    {
        [FormerlySerializedAs("_gameAssetsDataHolder")] [SerializeField] private GameAssetDataHolder gameAssetDataHolder;
        [SerializeField] private WindowsManager _windowsManager;

        private UnitManager _unitManager;
       private LocationManager _locationManager;

        private void Awake()
        {
            var windowFactory = new WindowAssetFactory(gameAssetDataHolder);
            _windowsManager.SetWindowFactory(windowFactory);

            _unitManager = new UnitManager(gameAssetDataHolder);
            _locationManager = new LocationManager(gameAssetDataHolder, _unitManager);
            _locationManager.LoadLocation("TestLocation");
        }

        private void Update()
        {
            _unitManager.Update();
        }
    }
}