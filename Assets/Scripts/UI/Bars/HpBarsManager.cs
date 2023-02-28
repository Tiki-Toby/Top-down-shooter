using System.Collections.Generic;
using AssetData;
using GameFlow.Client.Infrastructure;
using Units.UnitLogic;
using HairyEngine.HairyCamera;
using UnityEngine;
using Zenject;

namespace UI.Bars
{
    public class HpBarsManager : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        
        private IGameAssetData _gameAssetData;
        private IAssetInstantiator _assetInstantiator;
        private UnitManager _unitManager;
        private Dictionary<UnitController, HpBarsView> _views;

        [Inject]
        public void Construct(IGameAssetData gameAssetData,
            IAssetInstantiator assetInstantiator,
            UnitManager unitManager)
        {
            _gameAssetData = gameAssetData;
            _assetInstantiator = assetInstantiator;
            _unitManager = unitManager;
            _views = new Dictionary<UnitController, HpBarsView>();
        }

        public void Update()
        {
            var bottomLeft = CameraHandler.Instance.GameCamera.ViewportToWorldPoint(new Vector3(0, 0, CameraHandler.Instance.GameCamera.nearClipPlane));
            var topRight = CameraHandler.Instance.GameCamera.ViewportToWorldPoint(new Vector3(1, 1, CameraHandler.Instance.GameCamera.nearClipPlane));
            
            foreach (var unit in _unitManager)
            {
                if(unit.ViewController.UnitPosition.x > bottomLeft.x && unit.ViewController.UnitPosition.x < topRight.x ||
                   unit.ViewController.UnitPosition.y > bottomLeft.y && unit.ViewController.UnitPosition.y < topRight.y)
                {
                    if (_views.TryGetValue(unit, out var barView))
                        barView.transform.position = unit.ViewController.UnitPosition + Vector2.up * 1;
                    else
                        barView = InitHpBar(unit);
                    
                    barView.SetValue(unit.UnitDataController.HpRatio);
                }
            }
        }

        private HpBarsView InitHpBar(UnitController unit)
        {
            var hpBarsView = _gameAssetData.GetBarView(unit.UnitDataController.UnitType);
            HpBarsView barInstance = _assetInstantiator.Instantiate<HpBarsView>(hpBarsView, unit.ViewController.UnitPosition, Quaternion.identity, parent);
            _views.Add(unit, barInstance);

            return barInstance;
        } 
    }
}