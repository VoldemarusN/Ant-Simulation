using Core.Bug.Factory;
using UnityEngine;
using Zenject;

namespace Views.Bug.Factory
{
    public class AntFactoryInstaller : MonoInstaller
    {
        [SerializeField] private Transform _bugSpawnPoint;

        public override void InstallBindings()
        {
            Container.Bind<IAntFactory>().To<AntFactory>().AsSingle();
        }
    }
}