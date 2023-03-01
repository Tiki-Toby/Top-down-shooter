using Game.Ui.WindowManager;
using Location;
using UI.Bars;
using Units.UnitLogic;
using UnityEngine;

namespace AssetData
{
    public interface IGameAssetData
    {
        Sprite GetSpriteById(string assetId);
        AudioClip GetSoundReferenceById(string assetId);
        Window GetUiViewObjectReferenceById(string assetId);
        LocationView GetLocationObject(string locationObjectId);
        UnitView GetUnitView(EnumUnitType unitTypeId);
        HpBarView GetBarView(EnumUnitType unitTypeId);
    }
}