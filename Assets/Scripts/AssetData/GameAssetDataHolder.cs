using AssetManagement;
using AssetManagement.Extensions;
using Game.Ui.WindowManager;
using GameLogic.UnitLogic;
using Location;
using UI.Bars;
using UnityEngine;

namespace AssetData
{
    [CreateAssetMenu(fileName = "GameAssetsDataHolder", menuName = "GameAssetsData/GameAssetsDataHolder")]
    public class GameAssetDataHolder : ScriptableObject, IGameAssetData
    {
        #region Fields

        [SerializeField] private LocationObjectHolder _locationObjectHolder;
        [SerializeField] private UIViewsPrefabHolder _uiViewsPrefabHolder;
        [SerializeField] private SoundPrefabHolder _soundPrefabHolder;
        [SerializeField] private SpriteAssetHolder _spriteAssetHolder;
        [SerializeField] private UnitsAssetHolder _unitsAssetHolder;
        [SerializeField] private HpBarsHolder _hpBarsHolder;

        #endregion

        #region Methods IGameAssetData

        public Sprite GetSpriteById(string assetId)
        {
            ObjectReference spriteReference = _spriteAssetHolder.GetReferenceWithId(assetId);
            return spriteReference?.GetObject<Sprite>();
        }

        public ObjectReference GetSoundReferenceById(string assetId)
        {
            return _soundPrefabHolder.GetReferenceWithId(assetId);
        }

        public ObjectReference GetUiViewObjectReferenceById(string assetId)
        {
           return _uiViewsPrefabHolder.GetReferenceWithId(assetId);
        }

        public ObjectReference GetLocationObject(string locationObjectId)
        {
            return _locationObjectHolder.GetReferenceWithId(locationObjectId);
        }

        public ObjectReference GetUnitView(EnumUnitType unitTypeId)
        {
            string assetId = unitTypeId.ToString();
            return _unitsAssetHolder.GetReferenceWithId(assetId);
        }

        public ObjectReference GetBarView(EnumUnitType unitTypeId)
        {
            string assetId = unitTypeId.ToString();
            return _hpBarsHolder.GetReferenceWithId(assetId);
        }

        #endregion
    }
}