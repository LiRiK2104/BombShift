using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Life
{
    public class LifeSwitcher : MonoBehaviour
    {
        private const int MinIndex = -1;
    
        [SerializeField] private List<LifeSetting> _lifeSettings;
    
        private int _index = 0;

        public int LeftLives => _index + 1;
        public LifeSetting Setting { get; private set; }
    
        public event Action LifeChanged;

        private void Awake()
        {
            _index = _lifeSettings.Count - 1;
            UpdateLifeSetting();
        }

        private void Start()
        {
            UpdateLifeSetting();
        }

        public void Raise()
        {
            _index++;
            UpdateLifeSetting();
        }
    
        public void Lower()
        {
            _index--;
            UpdateLifeSetting();
        }

        private void UpdateLifeSetting()
        {
            ClampIndex();

            if (_index == MinIndex)
                Player.Instance.Exploder.Explode();
            else
                Setting = _lifeSettings[_index];
        
            LifeChanged?.Invoke();
        }
    
        private void ClampIndex()
        {
            _index = Mathf.Clamp(_index, MinIndex, _lifeSettings.Count - 1);
        }
    }
}
