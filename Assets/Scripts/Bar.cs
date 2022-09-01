using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Bar : MonoBehaviour
{
    private Slider _slider;
    private float _targetValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        SpeedSetter.Instance.SpeedChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        SpeedSetter.Instance.SpeedChanged -= OnValueChanged;
    }

    private void Start()
    {
        HardSetSliderValue();
    }

    private void HardSetSliderValue()
    {
        _slider.value = _targetValue;
    }
    
    private void OnValueChanged()
    {
        _targetValue = (float)SpeedSetter.Instance.SpeedLevel / SpeedSetter.Instance.SpeedsLevelsCount;
        StopCoroutine(UpdateBar());
        StartCoroutine(UpdateBar());
    }

    private IEnumerator UpdateBar()
    {
        while (Mathf.Abs(_slider.value - _targetValue) > 0)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, Time.deltaTime);
            yield return null;
        }
    }
}