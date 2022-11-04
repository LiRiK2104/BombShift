using Environment;
using Helpers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnvironmentCreatorInstaller : MonoInstaller
    {
        [SerializeField] private EnvironmentCreator _environmentCreatorPrefab;
        
        public override void InstallBindings()
        {
            var instance = DiContainerRef.Container.InstantiatePrefabForComponent<EnvironmentCreator>(_environmentCreatorPrefab,
                transform.position, Quaternion.identity, null);

            Container.Bind<EnvironmentCreator>().FromInstance(instance).AsSingle();
            Container.Bind<IInitializable>().FromInstance(instance);
        }
    }
}