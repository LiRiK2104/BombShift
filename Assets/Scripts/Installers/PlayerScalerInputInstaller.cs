using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerScalerInputInstaller : MonoInstaller
    {
        [SerializeField] private PlayerScalerInput _playerScalerInput;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerScalerInput>().FromInstance(_playerScalerInput).AsSingle();
        }
    }
}