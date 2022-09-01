using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create LifeSetting", fileName = "LifeSetting", order = 51)]
public class LifeSetting : ScriptableObject
{
    [SerializeField] [ColorUsage(false, true)] private Color _emissionColor;
    [SerializeField] private List<FX> _effects = new List<FX>();

    public Color EmissionColor => _emissionColor;
    
    public bool HasEffect(FX effect)
    {
        return _effects.Contains(effect);
    }
}
