using Core;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class SimulationSettingsInstaller : MonoInstaller
    {
        [SerializeField] private SimulationSettings _simulationSettings;

        public override void InstallBindings()
        {
            Container.Bind<SimulationSettings>().FromScriptableObject(_simulationSettings).AsSingle().NonLazy();
        }
    }
}