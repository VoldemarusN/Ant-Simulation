using Core.Bug.Strategies;
using UnityEngine;
using Views;

namespace Views.Bug.Strategies.Implementations
{
    public class BasicMoveStrategy : IMoveStrategy
    {
        private readonly BugView _bugView;
        private readonly float _speed;
        
        private FoodTarget _target;

        public BasicMoveStrategy(BugView bugView, float speed)
        {
            _bugView = bugView;
            _speed = speed;
        }

        public void SetTarget(FoodTarget target)
        {
            _target = target;
        }

        public void Move()
        {
            if (_target == null) return;

            _bugView.transform.position = Vector3.MoveTowards(
                _bugView.transform.position,
                _target.transform.position,
                _speed * Time.deltaTime);
        }
    }
}