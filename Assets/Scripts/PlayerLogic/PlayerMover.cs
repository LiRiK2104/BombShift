using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(
        typeof(Rigidbody), 
        typeof(Player))]
    public class PlayerMover : MonoBehaviour
    {
        private Player _player;
        private Rigidbody _rigidbody;
        private bool _needPush = true;
    
        public Rigidbody Rigidbody => _rigidbody;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            _player.Died += StopPushing;
        }

        private void OnDisable()
        {
            _player.Died -= StopPushing;
        }

        private void FixedUpdate()
        {
            if (_needPush == false || _player.SpeedSwitcher.Setting == null)
                return;
        
            Vector3 moveVector = transform.forward * _player.SpeedSwitcher.Setting.Speed;
            _rigidbody.velocity = new Vector3(moveVector.x, _rigidbody.velocity.y, moveVector.z);
        }
    
        public void Stop()
        {
            StopPushing();
            _rigidbody.velocity = Vector3.zero;
        }
    
        private void StopPushing()
        {
            _needPush = false;
        }
    }
}
