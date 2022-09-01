using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PlayerMaterial : MonoBehaviour
{
    private const string EmissionColorID = "_EmissionColor";
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        SpeedSetter.Instance.SpeedChanged += UpdateEmissionEntry;
    }

    private void OnDisable()
    {
        SpeedSetter.Instance.SpeedChanged -= UpdateEmissionEntry;
    }

    private void UpdateEmissionEntry()
    {
        StopCoroutine(UpdateEmission());
        StartCoroutine(UpdateEmission());
    }

    private IEnumerator UpdateEmission()
    {
        Color startColor = _renderer.material.GetColor(EmissionColorID);
        Color targetColor = SpeedSetter.Instance.Setting.EmissionColor;

        if (startColor == targetColor)
            yield break;
        
        float progress = 0;
        float speed = 2;
        
        while (progress < 1)
        {
            progress += Time.deltaTime * speed;
            Color stepColor = Color.Lerp(startColor, targetColor, progress);
            _renderer.material.SetColor(EmissionColorID, stepColor);

            yield return null;
        }
    }
}
