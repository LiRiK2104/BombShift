using ShopSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private Inventory _inventoryPrefab;
    
        public override void InstallBindings()
        {
            var instance =
                Container.InstantiatePrefabForComponent<Inventory>(_inventoryPrefab, transform.position, Quaternion.identity,
                    null);

            Container.Bind<Inventory>().FromInstance(instance).AsSingle();
            Container.Bind<IInitializable>().FromInstance(instance);
        }
    }
}