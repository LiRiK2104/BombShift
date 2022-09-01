using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Gate _gate;
    private int _layerAfterExplosion;
    private bool _collisionIsHappened;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _layerAfterExplosion = LayerMask.NameToLayer("Gate Block");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_collisionIsHappened == false && collision.gameObject.TryGetComponentInParent(out PlayerMover playerMover))
        {
            _collisionIsHappened = true;
            _gate.SlowDownPlayer();

            gameObject.layer = _layerAfterExplosion;
            _rigidbody.isKinematic = false;

            ShakeCamera();
            Explode(GetDirection());
        }
    }
    
    public void Init(Gate gate)
    {
        _gate = gate;
    }

    private void ShakeCamera()
    {
        float time = 0.5f;
        float intensity = 2;
        VirtualCameraHandler.Instance.StartShake(time, intensity);
    }

    private Vector3 GetDirection()
    {
        int minAngle = -45;
        int maxAngle = 45;
        float angle = Random.Range(minAngle, maxAngle);
        Vector3 direction = Vector3.forward.RotateAngle(angle);

        return direction;
    }

    private void Explode(Vector3 direction)
    {
        float force = 10;
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }
}
