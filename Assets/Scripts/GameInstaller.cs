using Core;
using Zenject;

namespace DefaultNamespace
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILevelInfo>().To<LevelInfo>().AsSingle().NonLazy();
            Container.BindInterfacesTo<Game>().AsSingle().NonLazy();
        }
    }
}