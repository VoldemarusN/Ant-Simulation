using UnityEngine;

namespace Views.Bug
{
    public class BugView : FoodTarget
    {
        public void UpdatePosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
    }
}