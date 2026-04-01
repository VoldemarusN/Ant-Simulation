using System;
using UnityEngine;
using uPools;
using Views;

namespace Core
{
    internal class VegetableFoodFactory
    {
        private readonly ObjectPoolBase<VegetableTarget> _pool;
        private readonly Exception _argumntException;

        public VegetableFoodFactory(ObjectPoolBase<VegetableTarget> pool)
        {
            _pool = pool;
        }

        public VegetableTarget Spawn(Vector3 position)
        {
            var instance = _pool.Rent();
            instance.transform.position = position;
            return instance;
        }

        public void Despawn(VegetableTarget instance)
        {
            _pool.Return(instance);
        }
    }
}