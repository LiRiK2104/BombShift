using System;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace RoundLogic.Start
{
    public class RoundStarter : MonoBehaviour
    {
        [SerializeField] private StartMenu _startMenu;
        
        [Inject] private PlayerScalerInput _playerScalerInput;

        public event Action RoundStarted;

        private bool _isStarted;

        public bool IsStarted => _isStarted;
        

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
