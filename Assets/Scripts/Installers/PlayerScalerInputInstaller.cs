using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerScalerInputInstaller : MonoInstaller
    {
        [SerializeField] private PlayerScalerInput _playerScalerInputPrefab;
        
        public override void InstallBindings()
        {
            var instance = Container.InstantiatePrefabForComponent<PlayerScalerInput>(_playerScalerInputPrefab);
            Container.Bind<PlayerScalerInput>().FromInstance(instance).AsSingle();
        }
    }
}