using UnityEngine;
using Zenject;

namespace Assets
{
    public class AssetProviderInstaller : MonoInstaller
    {
        [SerializeField] private AssetProvider _assetProvider;
        
        public override void InstallBindings()
        {
            Container.Bind<AssetProvider>().FromScriptableObject(_assetProvider).AsSingle().NonLazy();
        }
    }
}