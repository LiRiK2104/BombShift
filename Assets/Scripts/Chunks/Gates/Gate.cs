using System;
using System.Collections.Generic;
using PlayerLogic;
using PlayerLogic.Spirit;
using UnityEngine;
using Zenject;

namespace Chunks.Gates
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private GateSuccessTrigger _gateSuccessTrigger;
        [SerializeField] private List<GateBlock> _blocks = new List<GateBlock>();
        [SerializeField] private SpiritPoint _spiritPoint;

        [Inject] private Player _player;
        
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
        
            _player.SpeedSwitcher.Lower();
            _player.LifeSwitcher.Lower();
            MarkAsUsed();
        }

        public void SpeedUpPlayer()
        {
            if (_isUsed)
                return;
        
            _player.SpeedSwitcher.Raise();
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
