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
            var instance = Container.InstantiatePrefabForComponent<Shop>(_shopPrefab);
            
            Container.Bind<Shop>().FromInstance(instance).AsSingle();
            Container.Bind<IInitializable>().FromInstance(instance);
        }
    }
}