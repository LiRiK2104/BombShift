using UnityEngine;

public class TrampolineStartZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponentInParent(out PlayerMover playerMover))
        {
            VirtualCameraHandler.Instance.SlowDown();
            Player.Instance.GroundChecker.StopChecking();
            Player.Instance.Scaler.GoToIdleScale();
            SetConstraints(playerMover);
        }
    }
    
    private void SetConstraints(PlayerMover playerMover)
    {
        playerMover.Rigidbody.constraints = 
            RigidbodyConstraints.FreezePositionX | 
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
    }
}
