using Environment;
using PlayerLogic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Finish
{
    public class FinishPoint : MonoBehaviour
    {
        private const int MinLevel = 0;

        [SerializeField] private int _level;
        [SerializeField] private Transform _buildingSpawnPoint;
        [SerializeField] private MeshRenderer _groundMeshRenderer;

        [Inject] private Player _player;

        private void OnValidate()
        {
            _level = Mathf.Max(_level, MinLevel);
        }

        private void OnEnable()
        {
            EnvironmentCreator.Instance.Generate += CreateBuilding;
        }

        private void OnDisable()
        {
            EnvironmentCreator.Instance.Generate -= CreateBuilding;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBody playerBody))
                _player.LifeSwitcher.Lower();
        }

        private void CreateBuilding(EnvironmentPreset environmentPreset)
        {
            var preset = environmentPreset.GetFinishPointPreset(_level);
        
            Instantiate(preset.Building, _buildingSpawnPoint.position, quaternion.identity, transform);
            _groundMeshRenderer.material = preset.GroundMaterial;
        }
    }
}
