using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic.Life
{
    [CreateAssetMenu(menuName = "Create LifeSetting", fileName = "LifeSetting", order = 51)]
    public class LifeSetting : ScriptableObject
    {
        [SerializeField] [ColorUsage(false, true)] private Color _emissionColor;
        [SerializeField] private List<FX.FX> _effects = new List<FX.FX>();

        public Color EmissionColor => _emissionColor;
    
        public bool HasEffect(FX.FX effect)
        {
            return _effects.Contains(effect);
        }
    }
}
