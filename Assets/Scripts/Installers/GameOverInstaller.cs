using EndGame;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameOverInstaller : MonoInstaller
    {
        [SerializeField] private GameOver _gameOverPrefab;
        
        public override void InstallBindings()
        {
            var instance =
                Container.InstantiatePrefabForComponent<GameOver>(_gameOverPrefab, transform.position, Quaternion.identity,
                    null);

            Container.Bind<GameOver>().FromInstance(instance).AsSingle();
        }
    }
}