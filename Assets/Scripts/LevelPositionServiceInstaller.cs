using Core;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class LevelPositionServiceInstaller : MonoInstaller
    {
        [SerializeField] private Renderer _floorRenderer;
        
        public override void InstallBindings()
        {
            Container.Bind<LevelPositionService>().AsSingle().WithArguments(_floorRenderer).NonLazy();
        }
    }
}