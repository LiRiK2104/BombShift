using Progress;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProgressControllerInstaller : MonoInstaller
    {
        [SerializeField] private ProgressController _progressControllerPrefab;
        
        public override void InstallBindings()
        {
            var instance = Container.InstantiatePrefabForComponent<ProgressController>(_progressControllerPrefab);
            Container.Bind<ProgressController>().FromInstance(instance).AsSingle();
        }
    }
}