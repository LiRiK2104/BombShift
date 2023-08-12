using System;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace RoundLogic.Start
{
    public class RoundStarter : MonoBehaviour
    {
        [SerializeField] private StartMenu _startMenu;
        
        private PlayerScalerInput _playerScalerInput;
        private bool _isStarted;

        public event Action RoundStarted;

        public bool IsStarted => _isStarted;

        
        [Inject]
        private void Construct(PlayerScalerInput playerScalerInput)
        {
            _playerScalerInput = playerScalerInput;
        }

        private void OnEnable()
        {
            _playerScalerInput.BeginningDrag += TryStartRound;
            RoundStarted += _startMenu.Hide;
        }

        private void OnDisable()
        {
            _playerScalerInput.BeginningDrag -= TryStartRound;
            RoundStarted -= _startMenu.Hide;
        }

        private void TryStartRound()
        {
            if (_isStarted)
                return;
            
            _isStarted = true;
            RoundStarted?.Invoke();
        }
    }
}
