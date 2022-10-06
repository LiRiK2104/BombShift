using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic.Speed
{
    public class SpeedSwitcher : MonoBehaviour
    {
        [SerializeField] private List<SpeedSetting> _speedSettings = new List<SpeedSetting>();
        [SerializeField] private int _startSettingIndex;
        [SerializeField] private Player _player;

        private int _index = 0;

        public SpeedSetting Setting { get; private set; }
        public int SpeedsLevelsCount => _speedSettings.Count - 1;
        public int SpeedLevel => _index;
    
        public event Action SpeedChanged;
    
        private void Awake()
        {
            _index = _startSettingIndex;
        }
    
        private void OnEnable()
        {
            _player.Died += LowerFull;
        }

        private void OnDisable()
        {
            _player.Died -= LowerFull;
        }

        private void Start()
        {
            UpdateSpeedSetting();
        }
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Lower();
        
            if (Input.GetKeyDown(KeyCode.E))
                Raise();
        }

        public void Raise()
        {
            _index++;
            UpdateSpeedSetting();
        }
    
        public void Lower()
        {
            _index -= 3;
            UpdateSpeedSetting();
        }

        private void LowerFull()
        {
            _index = 0;
            UpdateSpeedSetting();
        }
    
        private void UpdateSpeedSetting()
        {
            ClampIndex();
            Setting = _speedSettings[_index];
            SpeedChanged?.Invoke();
        }
    
        private void ClampIndex()
        {
            _index = Mathf.Clamp(_index, 0, _speedSettings.Count - 1);
        }
    }
}
