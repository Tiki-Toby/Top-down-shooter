using AssetData;
using GameLogic.Core;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameAssetInstaller : MonoInstaller
    {
        [SerializeField] private GameAssetDataHolder gameAssetsDataHolder;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameAssetData>().FromInstance(gameAssetsDataHolder).AsSingle();
            Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
        }
    }
}