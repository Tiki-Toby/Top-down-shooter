using AssetData;
using GameFlow.Client.Infrastructure;
using Core;
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
            Container.Bind<IAssetInstantiator>().To<AssetInstantiator>().AsSingle();
            
            Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
        }
    }
}