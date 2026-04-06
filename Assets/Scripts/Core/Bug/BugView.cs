using Core.Food;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Bug
{
    public class BugView : FoodTarget
    {
        [SerializeField] private NavMeshAgent _agent;

        private void Awake()
        {
            _agent.enabled = false;
        }

        private void OnDisable()
        {
            _agent.enabled = false;
        }

        public void WarpTo(Vector3 newPosition)
        {
            _agent.enabled = false;
            transform.position = newPosition;
            _agent.enabled = true;
        }

        public void MoveTo(Vector3 newPosition)
        {
            _agent.SetDestination(newPosition);
        }

        public void SetAgentSpeed(float speed)
        {
            _agent.speed = speed;
        }
    }
}