using Helpers;
using SceneManagement;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoaderPrefab;
        
        public override void InstallBindings()
        {
            var instance = DiContainerRef.Container.InstantiatePrefabForComponent<SceneLoader>(_sceneLoaderPrefab);
            Container.Bind<SceneLoader>().FromInstance(instance).AsSingle();
        }
    }
}