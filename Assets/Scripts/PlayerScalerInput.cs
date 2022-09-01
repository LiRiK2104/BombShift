using UnityEngine;
using UnityEngine.EventSystems;

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
        Player.Instance.Scaler.SetScale(delta);
    }
}
