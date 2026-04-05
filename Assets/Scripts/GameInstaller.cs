using Core;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameUI _gameUI;

        public override void InstallBindings()
        {
            Container.Bind<ILevelInfo>().To<LevelInfo>().AsSingle().NonLazy();
            Container.BindInterfacesTo<Game>().AsSingle().NonLazy();
            Container.BindInstance(_gameUI).AsSingle();
        }
    }
}