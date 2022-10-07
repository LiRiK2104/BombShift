using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Canvas))]
public class TestCanvas : MonoBehaviour
{
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        SetUiCamera();
    }

    private void SetUiCamera()
    {
        var mainCameraData = Camera.main.GetUniversalAdditionalCameraData();
        
        if (mainCameraData.cameraStack != null && mainCameraData.cameraStack.Count > 0)
            _canvas.worldCamera = mainCameraData.cameraStack[0];
    }
}
