using Core.Services;
using Zenject;

namespace Core.Services
{
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFoodSpawner>().To<FoodSpawner>().AsSingle();
            Container.Bind<IBugManager>().To<BugManager>().AsSingle();
            Container.Bind<IUIUpdater>().To<UIUpdater>().AsSingle();
        }
    }
}