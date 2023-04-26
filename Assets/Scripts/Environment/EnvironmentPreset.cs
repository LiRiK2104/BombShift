using System;
using System.Collections.Generic;
using Environment.Buildings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment
{
    [CreateAssetMenu(menuName = "Environment Preset", fileName = "EnvironmentPreset", order = 51)]
    public class EnvironmentPreset : ScriptableObject
    {
        [SerializeField] private List<Material> _skyboxMaterials;
        [SerializeField] private FinishPointPreset[] _finishPointPresets = new FinishPointPreset[3];
        

        public Material GetRandomSkybox()
        {
            int index = Random.Range(0, _skyboxMaterials.Count);
            return _skyboxMaterials[index];
        }
        
        public FinishPointPreset GetFinishPointPreset(int level)
        {
            int index = level - 1;
        
            if (index < 0 || index >= _finishPointPresets.Length)
                index = 0;

            return _finishPointPresets[index];
        }
    }

    [Serializable]
    public class FinishPointPreset
    {
        [SerializeField] private Material _groundMaterial;
        [SerializeField] private Building _building;
    
        public Material GroundMaterial => _groundMaterial;
        public Building Building => _building;
    }
}