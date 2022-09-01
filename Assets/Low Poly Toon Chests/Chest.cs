using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    private static readonly int OpenState = Animator.StringToHash(ChestAnimator.Flags.IsOpen);

    [SerializeField] private Color _lightColor;
    
    private Animator _animator;

    public Color LightColor => _lightColor;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.SetBool(OpenState, true);
    }
}

public class ChestAnimator
{
    public class Flags
    {
        public const string IsOpen = "Open";
    }
}
