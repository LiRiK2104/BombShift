using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class PlayerUnitInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        
        public override void InstallBindings()
        {
            Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
            Container.QueueForInject(_player);
        }
    }
}