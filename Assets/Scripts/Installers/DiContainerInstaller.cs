using Helpers;
using Zenject;

namespace Installers
{
    public sealed class DiContainerInstaller : MonoInstaller
    {
        [Inject] private DiContainer _diContainer;
        
        public override void InstallBindings()
        {
            DiContainerRef.Container = _diContainer;

        }
    }
}