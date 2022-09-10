using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CinemachineSwitcher : Singleton<CinemachineSwitcher>
{
    [SerializeField] private VirtualCamera _followingCamera;
    [SerializeField] private VirtualCamera _finishSpectatingCamera;

    private Animator _animator;
    private VirtualCamera _activeCamera;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        
        _activeCamera = _followingCamera;
    }

    private void OnEnable()
    {
        Player.Instance.SpeedSwitcher.SpeedChanged += UpdateSpeedEffect;
        Player.Instance.Died += StopFollowing;
        Player.Instance.Died += StopLookingAt;
    }

    private void OnDisable()
    {
        Player.Instance.SpeedSwitcher.SpeedChanged -= UpdateSpeedEffect;
        Player.Instance.Died -= StopFollowing;
        Player.Instance.Died -= StopLookingAt;
    }

    public void Shake(float time, float intensity)
    {
        _activeCamera.StartShake(time, intensity);
    }
    
    public void Spectate()
    {
        _activeCamera = _finishSpectatingCamera;
        _animator.Play(CinemachineSwitcherAnimator.States.FinishSpectating);
    }

    private void StopFollowing()
    {
        _activeCamera.StopFollowing();
    }

    private void StopLookingAt()
    {
        _activeCamera.StopLookingAt();
    }

    private void UpdateSpeedEffect()
    {
        if (_activeCamera == _followingCamera)
        {
            _activeCamera.UpdateSpeedEffect();
        }
    }
}

public class CinemachineSwitcherAnimator
{
    public class States
    {
        public const string Following = "Following";
        public const string FinishSpectating = "Finish spectating";
    }
}
