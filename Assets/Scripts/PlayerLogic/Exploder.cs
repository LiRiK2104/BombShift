using System.Collections.Generic;
using Cinemachine;
using Environment.Buildings;
using Helpers;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(
        typeof(Rigidbody), 
        typeof(Player))]
    public class Exploder : MonoBehaviour
    {
        private const int MinExplosionForce = 0;
        private const int MaxExplosionForce = 5000;
        private const int MinExplosionRadius = 0;
        private const int MaxExplosionRadius = 30;
    
        private const float MinCameraShakeTime = 0;
        private const float MaxCameraShakeTime = 3;
        private const float MinCameraShakeIntensity = 0;
        private const float MaxCameraShakeIntensity = 10;

        [SerializeField] [Range(MinExplosionForce, MaxExplosionForce)] private float _force;
        [SerializeField] [Range(MinExplosionRadius, MaxExplosionRadius)] private float _radius;
    
        [SerializeField] [Range(MinCameraShakeTime, MaxCameraShakeTime)] private float _cameraShakeTime;
        [SerializeField] [Range(MinCameraShakeIntensity, MaxCameraShakeIntensity)] private float _cameraShakeIntensity;
    
        [SerializeField] private FX.Effect _explosionEffect;
        [SerializeField] private Transform _originPoint;
        
        [Inject] private CinemachineSwitcher _cinemachineSwitcher;
        
        private Player _player;
        private bool _isCollided;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isCollided == false && 
                collision.collider.TryGetComponent(out FracturableBuildingBlock block) &&
                gameObject.TryGetComponentInParent(out Player player))
            {
                _isCollided = true;
                Explode();
            }
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        public void Explode()
        {
            AddForce();
            PlayEffect();
            ShakeCamera();
        
            _player.Die();
        }

        private void AddForce()
        {
            Collider[] nearestColliders = Physics.OverlapSphere(_originPoint.position, _radius);
            List<BuildingBlock> usedBuildings = new List<BuildingBlock>();

            foreach (var collider in nearestColliders)
            {
                if (collider.TryGetComponent(out BuildingBlock buildingBlock) &&
                    usedBuildings.Contains(buildingBlock) == false &&
                    collider.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(_force, _originPoint.position, _radius);
                    buildingBlock.DestructSelf();
                
                    usedBuildings.Add(buildingBlock);
                }
            }
        }

        private void PlayEffect()
        {
            var effect = Instantiate(_explosionEffect, transform.position, quaternion.identity);
            effect.Play();
        }
    
        private void ShakeCamera()
        {
            _cinemachineSwitcher.Shake(_cameraShakeTime, _cameraShakeIntensity);
        }
    }
}
