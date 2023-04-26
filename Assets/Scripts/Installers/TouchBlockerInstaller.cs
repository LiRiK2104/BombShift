using Helpers;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TouchBlockerInstaller : MonoInstaller
    {
        [SerializeField] private TouchBlocker _touchBlockerPrefab;
        
        public override void InstallBindings()
        {
            var instance = DiContainerRef.Container.InstantiatePrefabForComponent<TouchBlocker>(_touchBlockerPrefab);
            Container.Bind<TouchBlocker>().FromInstance(instance).AsSingle();
        }
    }
}