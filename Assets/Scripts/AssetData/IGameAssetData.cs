using Game.Ui.WindowManager;
using GameLogic.UnitLogic;
using Location;
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
    }
}