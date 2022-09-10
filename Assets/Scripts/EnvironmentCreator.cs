using System;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCreator : Singleton<EnvironmentCreator>
{
    private static string _lastIndexKey = "last_index";
    
    [SerializeField] private List<EnvironmentPreset> _environmentPresets = new List<EnvironmentPreset>();
    
    public event Action<EnvironmentPreset> Generate;

    private int Index
    {
        get
        {
            int index = 0;
            
            if (PlayerPrefs.HasKey(_lastIndexKey))
            {
                index = PlayerPrefs.GetInt(_lastIndexKey);
                index++;

                if (index >= _environmentPresets.Count)
                    index = 0;
            }

            PlayerPrefs.SetInt(_lastIndexKey, index);
            return index;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        var preset = _environmentPresets[Index];
        Generate?.Invoke(preset);
        RenderSettings.skybox = preset.SkyboxMaterial;
    }
}
