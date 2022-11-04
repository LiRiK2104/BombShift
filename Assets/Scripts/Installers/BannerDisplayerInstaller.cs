using Helpers;
using UI.BannerSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BannerDisplayerInstaller : MonoInstaller
    {
        [SerializeField] private BannerDisplayer _bannerDisplayerPrefab;
        
        public override void InstallBindings()
        {
            var instance = DiContainerRef.Container.InstantiatePrefabForComponent<BannerDisplayer>(_bannerDisplayerPrefab);
            Container.Bind<BannerDisplayer>().FromInstance(instance).AsSingle().NonLazy();
        }
    }
}