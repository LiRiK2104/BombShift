using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem.DotContentLogic
{
    public class DotContent : MonoBehaviour
    {
        [SerializeField] private Dot _template;
        [SerializeField] private Color _enableColor;
        [SerializeField] private Color _disableColor;

        private List<Dot> _dots = new List<Dot>();
        private bool _isGenerated;

        public void GenerateDots(int count)
        {
            if (_isGenerated)
                return;
        
            count = Mathf.Max(0, count);

            for (int i = 0; i < count; i++)
            {
                var dot = Instantiate(_template, transform);
                _dots.Add(dot);
            }

            _isGenerated = true;
        }

        public void UpdateDots(int index)
        {
            index = Mathf.Clamp(index, 0, _dots.Count - 1);

            for (int i = 0; i < _dots.Count; i++)
            {
                _dots[i].SetColor(index == i ? _enableColor : _disableColor);
            }
        }
    }
}
