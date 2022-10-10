using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace PlayerLogic
{
    public class PlayerScalerInput : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        public event Action BeginningDrag;
        public event Action<float> Dragging; 
        
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginningDrag?.Invoke();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            float sensitivity = 0.005f;
            float delta = eventData.delta.y * sensitivity;
        
            Dragging?.Invoke(delta);
        }
    }
}
