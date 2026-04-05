using System;
using UnityEngine;

namespace Views.Bug
{
    [Serializable]
    public class PredatorSettings
    {
        public float Speed => _speed;
        public int FoodAmountToSplit => _foodAmountToSplit;
        public float LifetimeInSeconds => _lifetimeInSeconds;

        [SerializeField] private float _speed = 5f;
        [SerializeField] private int _foodAmountToSplit = 3;
        [SerializeField] private float _lifetimeInSeconds = 10f;
    }
}