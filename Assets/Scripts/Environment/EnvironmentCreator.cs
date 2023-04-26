using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Environment
{
    public class EnvironmentCreator : MonoBehaviour, IInitializable
    {
        private static string _lastIndexKey = "last_index";
    
        [SerializeField] private List<EnvironmentPreset> _environmentPresets = new List<EnvironmentPreset>();
    
        public event Action<EnvironmentPreset> Generating;

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
        
        public void Initialize()
        {
            Generate();
        }
        
        private void Generate()
        {
            var preset = _environmentPresets[Index];
            Generating?.Invoke(preset);
            RenderSettings.skybox = preset.GetRandomSkybox();
        }
    }
}
