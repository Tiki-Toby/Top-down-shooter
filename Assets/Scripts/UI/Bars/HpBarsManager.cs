using System;
using System.Collections.Generic;
using AssetData;
using HairyEngine.HairyCamera;
using Tools;
using Units.UnitLogic;
using Units.UnitLogic.Factory;
using UnityEngine;
using Zenject;

namespace UI.Bars
{
    public class HpBarsManager : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        
        private IGameAssetData _gameAssetData;
        private UnitManager _unitManager;
        private Dictionary<EnumUnitType, Stack<HpBarView>> _disabledViews;
        private Dictionary<UnitController, HpBarView> _views;

        [Inject]
        public void Construct(IGameAssetData gameAssetData,
            UnitManager unitManager)
        {
            _gameAssetData = gameAssetData;
            _unitManager = unitManager;
            _views = new Dictionary<UnitController, HpBarView>();
        
            _disabledViews = new Dictionary<EnumUnitType, Stack<HpBarView>>();

            foreach (EnumUnitType unitType in Enum.GetValues(typeof(EnumUnitType)))
            {
                if(unitType == EnumUnitType.Undefined)
                    continue;
                
                _disabledViews.Add(unitType, new Stack<HpBarView>());
            }
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
                }
            }
        }

        private HpBarView InitHpBar(UnitController unit)
        {
            var hpBarsView = GetHpBarView(unit);
            
            unit.UnitEventController.OnUnitTakeDamageSubscribe(_ => hpBarsView.SetValue(unit.UnitDataController.HpRatio));
            unit.UnitEventController.OnUnitAfterDeadSubscribe(() => ReleaseBar(unit));

            return hpBarsView;
        }

        private HpBarView GetHpBarView(UnitController unit)
        {
            if (!_disabledViews[unit.UnitDataController.UnitType].TryPop(out HpBarView view))
            {
                var hpBarsView = _gameAssetData.GetBarView(unit.UnitDataController.UnitType);
                view = GameObject.Instantiate<HpBarView>(hpBarsView, unit.ViewController.UnitPosition, Quaternion.identity, parent);
            }
            
            view.gameObject.SetActive(true);
            view.SetValue(unit.UnitDataController.HpRatio);
            _views.Add(unit, view);
                
            return view;
        }

        private void ReleaseBar(UnitController unit)
        {
            if(!_views.Remove(unit, out HpBarView view))
                return;

            view.gameObject.SetActive(false);
            _disabledViews[unit.UnitDataController.UnitType].Push(view);
        }
    }
}