using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace PlayerLogic
{
    public class PlayerScalerInput : MonoBehaviour, IDragHandler
    {
        [Inject] private Player _player;
        
        public void OnDrag(PointerEventData eventData)
        {
            float sensitivity = 0.005f;
            float delta = eventData.delta.y * sensitivity;
        
            SetScale(delta);
        }

        private void SetScale(float delta)
        {
            _player.Scaler.SetScale(delta);
        }
    }
}
