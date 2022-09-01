using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create SpeedSetting", fileName = "SpeedSetting", order = 51)]
public class SpeedSetting : ScriptableObject
{
    private const int MinSpeed = 7;
    private const int MaxSpeed = 17;
    private const int MinCameraDistance = 0;
    private const int MaxCameraDistance = 3;

    [SerializeField] [Range(MinSpeed, MaxSpeed)] private int _speed;
    [SerializeField] [Range(MinCameraDistance, MaxCameraDistance)] private float _cameraDistance;
    [SerializeField] private List<FX> _effects = new List<FX>();
    
    public int Speed => _speed;
    public float CameraDistance => _cameraDistance;


    public bool HasEffect(FX effect)
    {
        return _effects.Contains(effect);
    }
}
