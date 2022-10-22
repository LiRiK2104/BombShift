using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TouchBlockerInstaller : MonoInstaller
    {
        [SerializeField] private TouchBlocker _touchBlocker;
        
        public override void InstallBindings()
        {
            Container.Bind<TouchBlocker>().FromInstance(_touchBlocker).AsSingle();
        }
    }
}