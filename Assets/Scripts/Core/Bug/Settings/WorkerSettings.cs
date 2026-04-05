using System;
using UnityEngine;

namespace Views.Bug
{
    [Serializable]
    public class WorkerSettings
    {
        public float Speed => _speed;
        public int FoodAmountToSplit => _foodAmountToSplit;
        public float ChanceToCreatePredator => _chanceToCreatePredator;
        public int BugAmountToCreatePredator => _bugAmountToCreatePredator;

        [SerializeField] private float _speed = 5f;
        [SerializeField] private int _foodAmountToSplit = 2;
        [SerializeField, Range(0, 1)] private float _chanceToCreatePredator = 0.1f;
        [SerializeField] private int _bugAmountToCreatePredator = 10;
    }
}