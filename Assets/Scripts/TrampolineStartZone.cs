using UnityEngine;

public class TrampolineStartZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponentInParent(out PlayerMover playerMover))
        {
            CinemachineSwitcher.Instance.Spectate();
            Player.Instance.GroundChecker.StopChecking();
            Player.Instance.Scaler.GoToIdleScale();
        }
    }
}
