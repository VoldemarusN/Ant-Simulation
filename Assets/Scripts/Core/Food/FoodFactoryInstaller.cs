using Zenject;

namespace Core.Food
{
    public class FoodFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<VegetableFoodFactory>().AsSingle().NonLazy();
        }
    }
}