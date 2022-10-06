using UnityEngine;
using Zenject;

namespace PlayerLogic.Spirit
{
    public class Spirit : MonoBehaviour
    {
        [Inject] protected Player Player;
        
        private Vector3 _targetPosition;
    
        protected virtual void OnEnable()
        {
            Player.Scaler.ScaleChanged += SetXYScale;
        }

        protected virtual void OnDisable()
        {
            Player.Scaler.ScaleChanged -= SetXYScale;
        }
    
        protected virtual void Update()
        {
            SetPosition();
        }

        public void UpdateTargetPosition(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }
    
        protected virtual void SetPosition()
        {
            transform.position = _targetPosition;
        }

        private void SetXYScale(Vector3 newScale)
        {
            transform.localScale = new Vector3(newScale.x, newScale.y, transform.localScale.z);
        }
    }
}
