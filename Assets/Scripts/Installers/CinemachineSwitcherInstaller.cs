using Cinemachine;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CinemachineSwitcherInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineSwitcher _cinemachineSwitcher;
        public override void InstallBindings()
        {
            Container.Bind<CinemachineSwitcher>().FromInstance(_cinemachineSwitcher).AsSingle();
            Container.Bind<IInitializable>().FromInstance(_cinemachineSwitcher);
        }
    }
}