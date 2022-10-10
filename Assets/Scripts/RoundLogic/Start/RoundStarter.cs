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
        
        public event Action Starting;

        private bool _isStarted;

        private void OnEnable()
        {
            _playerScalerInput.BeginningDrag += TryStartRound;
            Starting += _startMenu.OnStart;
        }

        private void OnDisable()
        {
            _playerScalerInput.BeginningDrag -= TryStartRound;
            Starting -= _startMenu.OnStart;
        }

        private void TryStartRound()
        {
            if (_isStarted)
                return;
            
            _isStarted = true;
            Starting?.Invoke();
        }
    }
}
