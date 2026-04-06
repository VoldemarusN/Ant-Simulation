using UniRx;
using UnityEngine;

namespace Core.Bug.Behaviors
{
    public class EatOnContactBehavior : IBugBehavior
    {
        private const float EatDistance = 3f;
        private CompositeDisposable _disposable;

        public void Initialize(BugController owner)
        {
            _disposable = new CompositeDisposable();

            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (owner.CurrentTarget == null) return;

                    var distance = Vector3.Distance(
                        owner.CurrentTarget.transform.position,
                        owner.View.transform.position);

                    if (distance < EatDistance)
                    {
                        owner.CurrentTarget.Eat();
                        owner.CurrentTarget = null;
                        owner.FoodCount.Value++;
                    }
                })
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
