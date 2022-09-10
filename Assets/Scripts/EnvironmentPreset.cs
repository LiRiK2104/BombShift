using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment Preset", fileName = "EnvironmentPreset", order = 51)]
public class EnvironmentPreset : ScriptableObject
{
    [SerializeField] private Material _skyboxMaterial;
    [SerializeField] private FinishPointPreset[] _finishPointPresets = new FinishPointPreset[3];

    public Material SkyboxMaterial => _skyboxMaterial;

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
