using System;
using System.Collections.Generic;
using FX;
using PlayerLogic;
using PlayerLogic.Spirit;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using Zenject;

namespace Chunks.Gates
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private GateSuccessTrigger _gateSuccessTrigger;
        [SerializeField] private List<GateBlock> _blocks = new List<GateBlock>();
        [SerializeField] private SpiritPoint _spiritPoint;
        [SerializeField] private GateSpiritEffect _gateSpiritEffect;

        [Inject] private Player _player;
        
        private bool _isUsed;

        public static event Action Passed;

        private void Start()
        {
            _gateSuccessTrigger.Init(this);
            _blocks.ForEach(block => block.Init(this));
        }


        public void OnPass()
        {
            if (_isUsed)
                return;
            
            SpeedUpPlayer();
            _blocks.ForEach(block => block.MakeIntangible());
            _gateSpiritEffect.Play();
            MarkAsUsed();
        }
        
        public void OnHit()
        {
            if (_isUsed)
                return;

            SlowDownPlayer();
            MarkAsUsed();
        }

        private void SlowDownPlayer()
        {
            _player.SpeedSwitcher.Lower();
            _player.LifeSwitcher.Lower();
        }

        private void SpeedUpPlayer()
        {
            _player.SpeedSwitcher.Raise();
        }

        private void MarkAsUsed()
        {
            _isUsed = true;
        
            _spiritPoint.gameObject.SetActive(false);
            Passed?.Invoke();
        }
    }
}
