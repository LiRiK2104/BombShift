using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic.Life
{
    [CreateAssetMenu(menuName = "Create LifeSetting", fileName = "LifeSetting", order = 51)]
    public class LifeSetting : ScriptableObject
    {
        [SerializeField] [ColorUsage(false, true)] private Color _emissionColor;
        [SerializeField] private List<FX.Effect> _effects = new List<FX.Effect>();

        public Color EmissionColor => _emissionColor;
    
        public bool HasEffect(FX.Effect effect)
        {
            return _effects.Contains(effect);
        }
    }
}
