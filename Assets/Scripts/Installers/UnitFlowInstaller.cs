﻿using GameLogic.AttackLogic;
using GameLogic.UnitLogic;
using GameLogic.UnitLogic.Factory;
using Zenject;

namespace Installers
{
    public class UnitFlowInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UnitManager>().AsSingle();
            Container.Bind<AttackService>().AsSingle();
            Container.Bind<BulletManager>().AsSingle();
            Container.Bind<UnitFactoryCreator>().AsSingle();
        }
    }
}