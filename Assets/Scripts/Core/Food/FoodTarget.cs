using UniRx;
using UnityEngine;

namespace Core.Food
{
    public abstract class FoodTarget : MonoBehaviour
    {
        public Subject<FoodTarget> Eaten { get; private set; }

        protected virtual void OnEnable()
        {
            Eaten = new Subject<FoodTarget>();
        }

        public void Eat()
        {
            Eaten.OnNext(this);
            Eaten.OnCompleted();
        }
    }
}