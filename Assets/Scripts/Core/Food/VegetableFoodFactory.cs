using Assets;
using UnityEngine;

namespace Core.Food
{
    public class VegetableFoodFactory
    {
        private readonly ObjectPoolFacade<VegetableTarget> _pool;

        public VegetableFoodFactory(AssetProvider assetProvider)
        {
            _pool = new ObjectPoolFacade<VegetableTarget>(assetProvider.VegetablePrefab, 100);
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