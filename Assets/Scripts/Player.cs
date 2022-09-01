using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerScaler _scaler;
    [SerializeField] private GroundChecker _groundChecker;

    public event Action Died;

    public bool Alive { get; private set; } = true;
    public PlayerMover Mover => _mover;
    public PlayerScaler Scaler => _scaler;
    public GroundChecker GroundChecker => _groundChecker;

    
    public void Die()
    {
        if (Alive == false)
            return;
        
        Alive = false;
        Died?.Invoke();
    }
}
