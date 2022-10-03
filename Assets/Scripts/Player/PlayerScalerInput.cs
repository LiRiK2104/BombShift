using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerScalerInput : MonoBehaviour, IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            float sensitivity = 0.005f;
            float delta = eventData.delta.y * sensitivity;
        
            SetScale(delta);
        }

        private void SetScale(float delta)
        {
            global::Player.Player.Instance.Scaler.SetScale(delta);
        }
    }
}
