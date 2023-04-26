using Chunks;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ChunkSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private ChunkSpawner _chunkSpawner;
        
        public override void InstallBindings()
        {
            Container.Bind<ChunkSpawner>().FromInstance(_chunkSpawner).AsSingle().NonLazy();
            Container.Bind<IInitializable>().FromInstance(_chunkSpawner).NonLazy();
        }
    }
}