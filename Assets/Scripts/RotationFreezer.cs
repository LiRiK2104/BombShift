using UnityEngine;

public class RotationFreezer : MonoBehaviour
{
    private Quaternion _startRotation;

    private void Start()
    {
        _startRotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = _startRotation;
    }
}
