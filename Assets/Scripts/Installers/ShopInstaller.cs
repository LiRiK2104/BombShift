using ShopSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField] private Shop _shop;
        
        public override void InstallBindings()
        {
            Container.Bind<Shop>().FromInstance(_shop).AsSingle();
            Container.Bind<IInitializable>().FromInstance(_shop);
        }
    }
}