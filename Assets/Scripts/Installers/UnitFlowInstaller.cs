using BuffLogic;
using Units.AttackLogic;
using Units.UnitLogic;
using Units.UnitLogic.Factory;
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

            Container.Bind<BuffManager>().AsSingle();
        }
    }
}