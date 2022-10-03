using System.Collections;
using Player.Speed;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class Bar : MonoBehaviour
    {
        private Slider _slider;
        private float _targetValue;

        private SpeedSwitcher SpeedSwitcher => Player.Player.Instance.SpeedSwitcher;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            SpeedSwitcher.SpeedChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            SpeedSwitcher.SpeedChanged -= OnValueChanged;
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
            _targetValue = (float)SpeedSwitcher.SpeedLevel / SpeedSwitcher.SpeedsLevelsCount;
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
}