using Cinemachine;
using Helpers;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace RoundLogic.Finish.FinishField
{
    public class FinishPreparer : MonoBehaviour
    {
        [Inject] private Player _player;
        [Inject] private CinemachineSwitcher _cinemachineSwitcher;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponentInParent(out PlayerMover playerMover))
            {
                _cinemachineSwitcher.Spectate();
                _player.GroundChecker.StopChecking();
                _player.Scaler.GoToIdleScale();
            }
        }
    }
}
