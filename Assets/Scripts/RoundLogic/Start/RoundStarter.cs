using System;
using PlayerLogic;
using UI.BannerSystem;
using UnityEngine;
using Zenject;

namespace RoundLogic.Start
{
    public class RoundStarter : MonoBehaviour
    {
        [SerializeField] private StartMenu _startMenu;
        
        [Inject] private PlayerScalerInput _playerScalerInput;
        //[Inject] private BannersDisplayer _bannersDisplayer;
        
        public event Action RoundStarted;

        private bool _isStarted;

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

        private void Start()
        {
            //_bannersDisplayer.Display();
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
