using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Exploder : MonoBehaviour
{
    [SerializeField] private Transform _originPoint;

    private bool _isCollided;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided == false && 
            collision.collider.TryGetComponent(out BuildingBlock block) &&
            gameObject.TryGetComponentInParent(out Player player))
        {
            _isCollided = true;
            
            PlayEffect();
            Invoke(nameof(Explode), SpeedSetter.Instance.Setting.ExplosionDelay);
            Invoke(nameof(ShakeCamera), SpeedSetter.Instance.Setting.ExplosionDelay);
        }
    }

    private void Explode()
    {
        if (SpeedSetter.Instance.Setting.NeedExplosion == false)
            return;
        
        float force = SpeedSetter.Instance.Setting.ExplosionForce;
        float radius = SpeedSetter.Instance.Setting.ExplosionRadius;

        Collider[] nearestColliders = Physics.OverlapSphere(_originPoint.position, radius);

        foreach (var collider in nearestColliders)
        {
            if (collider.TryGetComponent(out BuildingBlock buildingBlock) && 
                collider.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(force, _originPoint.position, radius);
        }
        
        gameObject.SetActive(false);
    }

    private void PlayEffect()
    {
        if (SpeedSetter.Instance.Setting.TryGetExplosionEffect(out FX effectReference))
        {
            var effect = Instantiate(effectReference, transform.position, quaternion.identity);
            effect.Play();
        }
    }
    
    private void ShakeCamera()
    {
        if (SpeedSetter.Instance.Setting.NeedShakeCamera == false)
            return;
        
        float time = SpeedSetter.Instance.Setting.CameraShakeTime;
        float intensity = SpeedSetter.Instance.Setting.CameraShakeIntensity;
        VirtualCameraHandler.Instance.StartShake(time, intensity);
    }
}
