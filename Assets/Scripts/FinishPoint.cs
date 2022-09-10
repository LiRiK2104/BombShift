using System;
using Unity.Mathematics;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private const int MinLevel = 0;
    private const int MinLives = 0;

    [SerializeField] private int _level;
    [SerializeField] private int _needLives;
    [SerializeField] private Transform _buildingSpawnPoint;
    [SerializeField] private MeshRenderer _groundMeshRenderer;

    private void OnValidate()
    {
        _needLives = Mathf.Max(_needLives, MinLives);
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
        if (other.TryGetComponent(out PlayerBody playerBody) && 
            Player.Instance.LifeSwitcher.LeftLives == _needLives)
        {
            Player.Instance.Exploder.Explode();   
        }
    }

    private void CreateBuilding(EnvironmentPreset environmentPreset)
    {
        var preset = environmentPreset.GetFinishPointPreset(_level);
        
        Instantiate(preset.Building, _buildingSpawnPoint.position, quaternion.identity, transform);
        _groundMeshRenderer.material = preset.GroundMaterial;
    }
}
