using System.Collections.Generic;
using UnityEngine;

public class SpeedSetting : MonoBehaviour
{
    private const int MinSpeed = 7;
    private const int MaxSpeed = 17;
    
    private const int MinJumpHeight = 1;
    private const int MaxJumpHeight = 10;
    
    private const int MinCameraDistance = 0;
    private const int MaxCameraDistance = 3;
    
    private const int MinExplosionForce = 0;
    private const int MaxExplosionForce = 1000;
    private const int MinExplosionRadius = 0;
    private const int MaxExplosionRadius = 15;
    private const int MinExplosionDelay = 0;
    private const int MaxExplosionDelay = 3;
    
    private const float MinCameraShakeTime = 0;
    private const float MaxCameraShakeTime = 3;
    private const float MinCameraShakeIntensity = 0;
    private const float MaxCameraShakeIntensity = 10;

    [SerializeField] [Range(MinSpeed, MaxSpeed)] private int _speed;
    [SerializeField] [Range(MinJumpHeight, MaxJumpHeight)] private float _jumpHeight;
    [SerializeField] [Range(MinCameraDistance, MaxCameraDistance)] private float _cameraDistance;
    [SerializeField] private Transform _targetPoint;
    [Space]
    [SerializeField] [ColorUsage(false, true)] private Color _emissionColor;

    [Space] 
    [SerializeField] private bool _needExplosion;
    [SerializeField] [Range(MinExplosionForce, MaxExplosionForce)] private float _explosionForce;
    [SerializeField] [Range(MinExplosionRadius, MaxExplosionRadius)] private float _explosionRadius;
    [SerializeField] [Range(MinExplosionDelay, MaxExplosionDelay)] private float _explosionDelay;
    [Space] 
    [SerializeField] private bool _needShakeCamera;
    [SerializeField] [Range(MinCameraShakeTime, MaxCameraShakeTime)] private float _cameraShakeTime;
    [SerializeField] [Range(MinCameraShakeIntensity, MaxCameraShakeIntensity)] private float _cameraShakeIntensity;
    
    [SerializeField] private List<FX> _effects = new List<FX>();
    [SerializeField] private FX _explosionEffect;


    public int Speed => _speed;
    public float JumpHeight => _jumpHeight;
    public float CameraDistance => _cameraDistance;
    public Vector3 TargetPosition => _targetPoint.position;
    public Color EmissionColor => _emissionColor;
    
    public bool NeedExplosion => _needExplosion;
    public float ExplosionForce => _explosionForce;
    public float ExplosionRadius => _explosionRadius;
    public float ExplosionDelay => _explosionDelay;
    
    public bool NeedShakeCamera => _needShakeCamera;
    public float CameraShakeTime => _cameraShakeTime;
    public float CameraShakeIntensity => _cameraShakeIntensity;

    public bool HasEffect(FX effect)
    {
        return _effects.Contains(effect);
    }

    public bool TryGetExplosionEffect(out FX explosionEffect)
    {
        explosionEffect = _explosionEffect;
        return _explosionEffect != null;
    }
}
