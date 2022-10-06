using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(typeof(Player))]
    public class PlayerScaler : MonoBehaviour
    {
        [SerializeField] private Transform _playerBody;
        [SerializeField] private Vector3 _scaleA;
        [SerializeField] private Vector3 _scaleB;
        
        private Player _player;
        private bool _handleScalingEnable = true;
        private float _scaleProgress = 0.5f;

        public event Action<Vector3> ScaleChanged;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }
        
        private void OnEnable()
        {
            _player.Died += OnDied;
        }

        private void OnDisable()
        {
            _player.Died -= OnDied;
        }

        public void SetScale(float delta)
        {
            if (_handleScalingEnable == false)
                return;

            _scaleProgress += delta;
            _scaleProgress = Mathf.Clamp(_scaleProgress, 0, 1);

            _playerBody.localScale = Vector3.Lerp(_scaleA, _scaleB, _scaleProgress);
            ScaleChanged?.Invoke(_playerBody.localScale);
        }

        public void GoToIdleScale()
        {
            _handleScalingEnable = false;
            StartCoroutine(GoToIdleScaleCore());
        }

        private void OnDied()
        {
            _handleScalingEnable = false;
        }

        private IEnumerator GoToIdleScaleCore()
        {
            float middleValue = 0.5f;
            Vector3 targetScale = Vector3.Lerp(_scaleA, _scaleB, middleValue);
        
            while (_playerBody.localScale != targetScale)
            {
                _playerBody.localScale = Vector3.MoveTowards(_playerBody.localScale, targetScale, Time.deltaTime);
                yield return null;
            }
        }
    }
}
