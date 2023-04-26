using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
    public class CustomSlider : MonoBehaviour
    {
        private const int MinProgress = 0;
        private const int MaxProgress = 1;
    
        [SerializeField] private Image _fillZone;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] [Range(MinProgress, MaxProgress)] private float _progress;

        private int _minValue = 0;
        private int _maxValue = 1;
    
        private void OnValidate()
        {
            UpdateFillZone();
        }

        public void Initialize(int maxValue, int progressValue)
        {
            _maxValue = Mathf.Max(_minValue, maxValue);
            SetValue(progressValue);
        }

        public void SetValue(int value)
        {
            _progress = Mathf.InverseLerp(_minValue, _maxValue, value);
            _textMeshPro.text = GetProgressString(value);
            UpdateFillZone();
        }

        private string GetProgressString(int value)
        {
            return $"{value}/{_maxValue}";
        }

        private void UpdateFillZone()
        {
            _fillZone.fillAmount = _progress;
        }
    }
}
