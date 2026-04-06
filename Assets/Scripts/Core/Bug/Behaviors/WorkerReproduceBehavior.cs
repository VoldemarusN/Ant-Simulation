using System;
using Core.Bug.Factory;
using Core.Bug.Settings;
using Core.Level;
using UniRx;
using Random = UnityEngine.Random;

namespace Core.Bug.Behaviors
{
    public class WorkerReproduceBehavior : IBugBehavior
    {
        private readonly WorkerSettings _settings;
        private readonly ILevelInfo _levelInfo;
        private readonly IAntFactory _factory;
        private CompositeDisposable _disposable;

        public WorkerReproduceBehavior(WorkerSettings settings, ILevelInfo levelInfo, IAntFactory factory)
        {
            _settings = settings;
            _levelInfo = levelInfo;
            _factory = factory;
        }

        public void Initialize(BugController owner)
        {
            _disposable = new CompositeDisposable();

            owner.FoodCount
                .Where(count => count >= _settings.FoodAmountToSplit)
                .Subscribe(_ =>
                {
                    owner.FoodCount.Value = 0;

                    BugController newBug;
                    if (_levelInfo.Workers.Count > _settings.BugAmountToCreatePredator
                        && Random.Range(0, 100) < _settings.ChanceToCreatePredator * 100)
                    {
                        newBug = _factory.CreatePredatorBug();
                    }
                    else
                    {
                        newBug = _factory.CreateWorkerBug();
                    }

                    owner.Reproduced.OnNext(newBug);
                })
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
