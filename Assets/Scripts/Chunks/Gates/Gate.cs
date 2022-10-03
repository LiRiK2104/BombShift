using System;
using System.Collections.Generic;
using PlayerLogic.Spirit;
using UnityEngine;

namespace Chunks.Gates
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private GateSuccessTrigger _gateSuccessTrigger;
        [SerializeField] private List<GateBlock> _blocks = new List<GateBlock>();
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
        
            PlayerLogic.Player.Instance.SpeedSwitcher.Lower();
            PlayerLogic.Player.Instance.LifeSwitcher.Lower();
            MarkAsUsed();
        }

        public void SpeedUpPlayer()
        {
            if (_isUsed)
                return;
        
            PlayerLogic.Player.Instance.SpeedSwitcher.Raise();
            MarkAsUsed();
        }

        private void MarkAsUsed()
        {
            _isUsed = true;
        
            _spiritPoint.gameObject.SetActive(false);
            Passed?.Invoke();
        }
    }
}
