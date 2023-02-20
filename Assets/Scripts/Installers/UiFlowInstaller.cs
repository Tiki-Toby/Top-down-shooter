using Game.Ui.WindowManager;
using UI.Bars;
using Zenject;

namespace AssetData.Installers
{
    public class UiFlowInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HpBarsManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<WindowsManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}