using DataBaseSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DataBaseInstaller : MonoInstaller
    {
        [SerializeField] private DataBase _dataBasePrefab;
    
        public override void InstallBindings()
        {
            var instance =
                Container.InstantiatePrefabForComponent<DataBase>(_dataBasePrefab, transform.position, Quaternion.identity,
                    null);

            Container.Bind<DataBase>().FromInstance(instance).AsSingle();
        }
    }
}