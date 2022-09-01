using UnityEngine;

public class TrampolineZone : MonoBehaviour
{
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponentInParent(out PlayerMover playerMover))
        {
            playerMover.Stop();
            Jump(playerMover);
            VirtualCameraHandler.Instance.SlowDown();
        }
    }

    private void Jump(PlayerMover playerMover)
    {
        Vector3 vector = 
            CalculateTrajectoryVelocity( playerMover.Rigidbody.transform.position, 
                SpeedSwitcher.Instance.Setting.TargetPosition, 
                SpeedSwitcher.Instance.Setting.JumpHeight);
        
        playerMover.Rigidbody.velocity = vector;
        playerMover.Rigidbody.AddTorque(3, 0, 0);
    }

    private Vector3 CalculateTrajectoryVelocity(Vector3 origin, Vector3 target, float t)
    {
        float x = (target.x - origin.x) / t;
        float z = (target.z - origin.z) / t;
        float y = ((target.y - origin.y) - 0.5f * Physics.gravity.y * t * t) / t;
        
        return new Vector3(x, y, z);
    }*/
}
