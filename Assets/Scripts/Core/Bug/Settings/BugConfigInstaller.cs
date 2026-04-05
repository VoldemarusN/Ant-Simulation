using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Views.Bug
{
    [CreateAssetMenu(fileName = "BugConfig", menuName = "ScriptableObjects/BugConfig")]
    public class BugConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private WorkerSettings _workerSettings;
        [SerializeField] private PredatorSettings _predatorSettings;

        public WorkerSettings WorkerSettings => _workerSettings;
        public PredatorSettings PredatorSettings => _predatorSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_workerSettings);
            Container.BindInstance(_predatorSettings);
        }
    }
}