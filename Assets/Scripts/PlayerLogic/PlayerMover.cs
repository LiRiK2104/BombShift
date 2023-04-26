using System;
using System.Collections;
using RoundLogic;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(
        typeof(Rigidbody), 
        typeof(Player))]
    public class PlayerMover : MonoBehaviour
    {
        [Inject] private RoundRunner _roundRunner;

        private Player _player;
        private Rigidbody _rigidbody;
        private bool _needPush;
    
        public Rigidbody Rigidbody => _rigidbody;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            _roundRunner.Starter.RoundStarted += StartPushing;
            _player.Died += StopPushing;
        }

        private void OnDisable()
        {
            _roundRunner.Starter.RoundStarted -= StartPushing;
            _player.Died -= StopPushing;
        }


        private void FixedUpdate()
        {
            if (_needPush == false || _player.SpeedSwitcher.Setting == null)
                return;
        
            Vector3 moveVector = transform.forward * _player.SpeedSwitcher.Setting.Speed;
            _rigidbody.velocity = new Vector3(moveVector.x, _rigidbody.velocity.y, moveVector.z);
        }

        private void StartPushing()
        {
            _needPush = true;
        }
    
        private void StopPushing()
        {
            _needPush = false;
        }
    }
}
