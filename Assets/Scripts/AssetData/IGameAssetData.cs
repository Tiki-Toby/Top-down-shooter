using AssetManagement;
using Units.UnitLogic;
using UnityEngine;

namespace AssetData
{
    public interface IGameAssetData
    {
        Sprite GetSpriteById(string assetId);
        ObjectReference GetSoundReferenceById(string assetId);
        ObjectReference GetUiViewObjectReferenceById(string assetId);
        ObjectReference GetLocationObject(string locationObjectId);
        ObjectReference GetUnitView(EnumUnitType unitTypeId);
        ObjectReference GetBarView(EnumUnitType unitTypeId);
    }
}