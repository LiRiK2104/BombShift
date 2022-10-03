using Cinemachine;
using Helpers;
using Player;
using UnityEngine;

namespace Finish
{
    public class FinishPreparer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponentInParent(out PlayerMover playerMover))
            {
                CinemachineSwitcher.Instance.Spectate();
                Player.Player.Instance.GroundChecker.StopChecking();
                Player.Player.Instance.Scaler.GoToIdleScale();
            }
        }
    }
}
