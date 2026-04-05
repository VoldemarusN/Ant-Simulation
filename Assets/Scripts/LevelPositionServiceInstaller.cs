using Core;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class LevelPositionServiceInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _floor;
        
        public override void InstallBindings()
        {
            Container.Bind<LevelPositionService>().AsSingle().WithArguments(_floor).NonLazy();
        }
    }
}