using UnityEngine;

namespace ShopSystem.Cells
{
    [RequireComponent(typeof(Animator))]
    public class CellView : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int HiddenTrigger = Animator.StringToHash(ShopCellAnimator.Triggers.StayHidden);
        private static readonly int ShowTrigger = Animator.StringToHash(ShopCellAnimator.Triggers.Show);
        private static readonly int UnlockTrigger = Animator.StringToHash(ShopCellAnimator.Triggers.Unlock);
        private static readonly int RouletteSelectedFlag = Animator.StringToHash(ShopCellAnimator.Flags.RouletteSelected);

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void StayHidden()
        {
            _animator.SetTrigger(HiddenTrigger);
        }
        
        public void Show()
        {
            _animator.SetTrigger(ShowTrigger);
        }
        
        public void RouletteSelect()
        {
            _animator.SetBool(RouletteSelectedFlag, true);
        }
        
        public void RouletteDeselect()
        {
            _animator.SetBool(RouletteSelectedFlag, false);
        }
        
        public void Unlock()
        {
            _animator.SetTrigger(UnlockTrigger);
        }
    }
}

public static class ShopCellAnimator
{
    public static class Triggers
    {
        public const string Show = nameof(Show);
        public const string StayHidden = nameof(StayHidden);
        public const string Unlock = nameof(Unlock);
    }
    
    public static class Flags
    {
        public const string RouletteSelected = nameof(RouletteSelected);
    }
}
