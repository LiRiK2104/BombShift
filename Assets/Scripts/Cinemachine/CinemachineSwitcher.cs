using Helpers;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Cinemachine
{
    [RequireComponent(typeof(Animator))]
    public class CinemachineSwitcher : MonoBehaviour, IInitializable
    {
        [SerializeField] private VirtualCamera _followingCamera;
        [SerializeField] private VirtualCamera _finishSpectatingCamera;
        
        [Inject] private Player _player;

        private Animator _animator;
        private VirtualCamera _activeCamera;
        

        private void OnEnable()
        {
            _player.SpeedSwitcher.SpeedChanged += UpdateSpeedEffect;
            _player.Died += StopFollowing;
            _player.Died += StopLookingAt;
        }

        private void OnDisable()
        {
            _player.SpeedSwitcher.SpeedChanged -= UpdateSpeedEffect;
            _player.Died -= StopFollowing;
            _player.Died -= StopLookingAt;
        }
        
        public void Initialize()
        {
            _animator = GetComponent<Animator>();
            _activeCamera = _followingCamera;
        }

        public void Shake(float time, float intensity)
        {
            _activeCamera.StartShake(time, intensity);
        }
    
        public void Spectate()
        {
            _activeCamera = _finishSpectatingCamera;
            _animator.Play(CinemachineSwitcherAnimator.States.FinishSpectating);
        }

        private void StopFollowing()
        {
            _activeCamera.StopFollowing();
        }

        private void StopLookingAt()
        {
            _activeCamera.StopLookingAt();
        }

        private void UpdateSpeedEffect()
        {
            if (_activeCamera == _followingCamera)
            {
                _activeCamera.UpdateSpeedEffect();
            }
        }
    }

    public class CinemachineSwitcherAnimator
    {
        public class States
        {
            public const string Following = "Following";
            public const string FinishSpectating = "Finish spectating";
        }
    }
}