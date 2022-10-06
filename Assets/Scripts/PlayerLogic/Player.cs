using System;
using PlayerLogic.Life;
using PlayerLogic.Speed;
using UnityEngine;

namespace PlayerLogic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private PlayerScaler _scaler;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private LifeSwitcher _lifeSwitcher;
        [SerializeField] private SpeedSwitcher _speedSwitcher;
        [SerializeField] private Exploder _exploder;

        public event Action Died;

        public bool Alive { get; private set; } = true;
        public PlayerMover Mover => _mover;
        public PlayerScaler Scaler => _scaler;
        public GroundChecker GroundChecker => _groundChecker;
        public LifeSwitcher LifeSwitcher => _lifeSwitcher;
        public SpeedSwitcher SpeedSwitcher => _speedSwitcher;
        public Exploder Exploder => _exploder;

    
        public void Die(bool needHide = true)
        {
            if (Alive == false)
                return;
        
            Alive = false;
            Died?.Invoke();
        
            if (needHide)
                gameObject.SetActive(false);
        }
    }
}
