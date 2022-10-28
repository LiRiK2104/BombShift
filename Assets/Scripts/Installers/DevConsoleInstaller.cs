using Dev;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DevConsoleInstaller : MonoInstaller
    {
        [SerializeField] private DevConsole _devConsolePrefab;
        
        public override void InstallBindings()
        {
            var instance = Container.InstantiatePrefabForComponent<DevConsole>(_devConsolePrefab);

            Container.Bind<DevConsole>().FromInstance(instance).AsSingle();
            Container.Bind<IInitializable>().FromInstance(instance);
        }
    }
}