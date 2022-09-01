using UnityEngine;

public class Spirit : MonoBehaviour
{
    private Vector3 _targetPosition;
    
    protected virtual void OnEnable()
    {
        Player.Instance.Scaler.ScaleChanged += SetXYScale;
    }

    protected virtual void OnDisable()
    {
        Player.Instance.Scaler.ScaleChanged -= SetXYScale;
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
