using UnityEngine;
using Zenject;

namespace Core.Bug.Settings
{
    [CreateAssetMenu(fileName = "BugConfig", menuName = "ScriptableObjects/BugConfig")]
    public class BugConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private WorkerSettings _workerSettings;
        [SerializeField] private PredatorSettings _predatorSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_workerSettings);
            Container.BindInstance(_predatorSettings);
        }
    }
}