using RoundLogic;
using RoundLogic.Finish;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class RoundRunnerInstaller : MonoInstaller
    {
        [SerializeField] private RoundRunner roundRunnerPrefab;
        
        public override void InstallBindings()
        {
            var instance =
                Container.InstantiatePrefabForComponent<RoundRunner>(roundRunnerPrefab, transform.position, Quaternion.identity,
                    null);

            Container.Bind<RoundRunner>().FromInstance(instance).AsSingle();
        }
    }
}