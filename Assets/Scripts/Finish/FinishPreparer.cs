using Cinemachine;
using Helpers;
using PlayerLogic;
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
                PlayerLogic.Player.Instance.GroundChecker.StopChecking();
                PlayerLogic.Player.Instance.Scaler.GoToIdleScale();
            }
        }
    }
}
