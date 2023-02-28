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

        [SerializeField] private LocationObjectHolder _locationObjectReferencesHolder;
        [SerializeField] private UIViewsPrefabHolder _uiViewsPrefabHolder;
        [SerializeField] private SoundPrefabHolder _soundPrefabHolder;
        [SerializeField] private SpriteAssetHolder _spriteAssetHolder;
        [SerializeField] private UnitsAssetHolder _unitsAssetHolder;
        [SerializeField] private HpBarsHolder _hpBarsHolder;

        #endregion

        #region Methods IGameAssetData

        public Sprite GetSpriteById(string assetId)
        {
            return _spriteAssetHolder.GetReferenceWithId(assetId);
        }

        public AudioClip GetSoundReferenceById(string assetId)
        {
            return _soundPrefabHolder.GetReferenceWithId(assetId);
        }

        public Window GetUiViewObjectReferenceById(string assetId)
        {
            return _uiViewsPrefabHolder.GetReferenceWithId(assetId);
        }

        public LocationView GetLocationObject(string locationObjectId)
        {
            return _locationObjectReferencesHolder.GetReferenceWithId(locationObjectId);
        }

        public UnitView GetUnitView(EnumUnitType unitTypeId)
        {
            string assetId = unitTypeId.ToString();
            return _unitsAssetHolder.GetReferenceWithId(assetId);
        }


        public HpBarView GetBarView(EnumUnitType unitTypeId)
        {
            string assetId = unitTypeId.ToString();
            return _hpBarsHolder.GetReferenceWithId(assetId);
        }

        #endregion
    }
}