using System;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GateSuccessTrigger _gateSuccessTrigger;
    [SerializeField] private List<Block> _blocks = new List<Block>();
    [SerializeField] private SpiritPoint _spiritPoint;
    
    private bool _isUsed;

    public static event Action Passed;

    private void Start()
    {
        _gateSuccessTrigger.Init(this);
        _blocks.ForEach(block => block.Init(this));
    }

    public void SlowDownPlayer()
    {
        if (_isUsed)
            return;
        
        SpeedSetter.Instance.Lower();
        MarkAsUsed();
    }

    public void SpeedUpPlayer()
    {
        if (_isUsed)
            return;
        
        SpeedSetter.Instance.Raise();
        MarkAsUsed();
    }

    private void MarkAsUsed()
    {
        _isUsed = true;
        
        _spiritPoint.gameObject.SetActive(false);
        Passed?.Invoke();
    }
}
