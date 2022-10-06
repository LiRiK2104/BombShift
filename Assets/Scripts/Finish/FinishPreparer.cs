using Cinemachine;
using Helpers;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Finish
{
    public class FinishPreparer : MonoBehaviour
    {
        [Inject] private Player _player;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponentInParent(out PlayerMover playerMover))
            {
                CinemachineSwitcher.Instance.Spectate();
                _player.GroundChecker.StopChecking();
                _player.Scaler.GoToIdleScale();
            }
        }
    }
}
