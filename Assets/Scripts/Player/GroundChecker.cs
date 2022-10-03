using UnityEngine;

namespace Player
{
    public class GroundChecker : MonoBehaviour
    {
        private bool _active = true;
        private Vector3 _boxCastCenter = Vector3.zero;
    
        private void FixedUpdate()
        {
            if (_active && HasGround() == false)
            {
                global::Player.Player.Instance.Die(false);
                SetConstraints();
            }
        }

        public void StopChecking()
        {
            _active = false;
        }

        private bool HasGround()
        {
            Vector3 centerDelay = Vector3.up;
            Vector3 center = gameObject.transform.position + centerDelay;
            Vector3 boxHalfSize = transform.localScale / 2;
            Vector3 direction = Vector3.down;
            float distance = 3;
            int layerMask = LayerMask.GetMask("Ground");
        
            bool result = Physics.BoxCast(center, boxHalfSize, direction, 
                Quaternion.identity, distance, layerMask);

            _active = result;
            _boxCastCenter = center + direction * distance;

            return result;
        } 

        private void SetConstraints()
        {
            global::Player.Player.Instance.Mover.Rigidbody.constraints = 
                RigidbodyConstraints.FreezePositionX | 
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(_boxCastCenter, transform.localScale);
        }
    }
}
