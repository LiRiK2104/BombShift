using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSetter : Singleton<SpeedSetter>
{
    [SerializeField] private List<SpeedSetting> _speedSettings = new List<SpeedSetting>();
    [SerializeField] private int _startSettingIndex;

    private int _index = 0;

    public SpeedSetting Setting { get; private set; }
    public int SpeedsLevelsCount => _speedSettings.Count - 1;
    public int SpeedLevel => _index;
    
    public event Action SpeedChanged;
    
    protected override void Awake()
    {
        base.Awake();
        _index = _startSettingIndex;
    }
    
    private void OnEnable()
    {
        Player.Instance.Died += LowerFull;
    }

    private void OnDisable()
    {
        Player.Instance.Died -= LowerFull;
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
        if (_index == _speedSettings.Count - 1)
        {
            LifesCounterTest.Instance.Raise();
        }
        
        _index++;
        UpdateSpeedSetting();
    }
    
    public void Lower()
    {
        LifesCounterTest.Instance.Lower();
        
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
