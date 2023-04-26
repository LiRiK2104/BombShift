using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SkinStorageInstaller : MonoInstaller
    {
        [SerializeField] private SkinStorage _skinStoragePrefab;
        
        public override void InstallBindings()
        {
            var instance = Container.InstantiatePrefabForComponent<SkinStorage>(_skinStoragePrefab);
            Container.Bind<SkinStorage>().FromInstance(instance).AsSingle();
        }
    }
}