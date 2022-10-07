using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SkinSetterInstaller : MonoInstaller
    {
        [SerializeField] private SkinSetter _skinSetterPrefab;
    
        public override void InstallBindings()
        {
            var instance =
                Container.InstantiatePrefabForComponent<SkinSetter>(_skinSetterPrefab, transform.position, Quaternion.identity,
                    null);

            Container.Bind<SkinSetter>().FromInstance(instance).AsSingle();
            Container.Bind<IInitializable>().FromInstance(instance);
        }
    }
}