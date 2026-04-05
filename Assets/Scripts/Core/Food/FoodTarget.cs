using UniRx;
using UnityEngine;

namespace Views
{
    public abstract class FoodTarget : MonoBehaviour
    {
        public Subject<FoodTarget> Eaten { get; private set; } = new();

        public void Eat()
        {
            Eaten.OnNext(this);
        }
    }
}