using System.Collections;
using PlayerLogic;
using PlayerLogic.Speed;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class Bar : MonoBehaviour
    {
        [Inject] private Player _player;
        
        private Slider _slider;
        private float _targetValue;

        private SpeedSwitcher SpeedSwitcher => _player.SpeedSwitcher;

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