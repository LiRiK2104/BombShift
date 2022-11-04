using Helpers;
using ShopSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField] private Shop _shopPrefab;
        
        public override void InstallBindings()
        {
            var instance = DiContainerRef.Container.InstantiatePrefabForComponent<Shop>(_shopPrefab);
            
            Container.Bind<Shop>().FromInstance(instance).AsSingle().NonLazy();
            Container.Bind<IInitializable>().FromInstance(instance).NonLazy();
        }
    }
}