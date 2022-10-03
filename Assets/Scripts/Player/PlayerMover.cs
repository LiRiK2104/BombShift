using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private bool _needPush = true;
    
        public Rigidbody Rigidbody => _rigidbody;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            global::Player.Player.Instance.Died += StopPushing;
        }

        private void OnDisable()
        {
            global::Player.Player.Instance.Died -= StopPushing;
        }

        private void FixedUpdate()
        {
            if (_needPush == false || global::Player.Player.Instance.SpeedSwitcher.Setting == null)
                return;
        
            Vector3 moveVector = transform.forward * global::Player.Player.Instance.SpeedSwitcher.Setting.Speed;
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