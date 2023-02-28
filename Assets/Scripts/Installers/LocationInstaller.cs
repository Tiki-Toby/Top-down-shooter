using Core;
using Location;
using Zenject;

namespace Installers
{
    public class LocationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LocationManager>().AsSingle();
        }
    }
}