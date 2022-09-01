using UnityEngine;

public class StopZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponentInParent(out Rigidbody rigidbody))
            rigidbody.isKinematic = true;
    }
}
