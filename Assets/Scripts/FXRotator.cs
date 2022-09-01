using UnityEngine;

public class FXRotator : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private Vector3 _lastPosition = Vector3.zero;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 direction = (_lastPosition - transform.position).normalized;
        _effect.transform.rotation = Quaternion.LookRotation(direction, transform.up);

        _lastPosition = transform.position;
        DrawRay(direction);
    }

    private void DrawRay(Vector3 direction)
    {
        float distance = 2;
        Debug.DrawRay(transform.position, direction * distance, Color.red);
    }
}
