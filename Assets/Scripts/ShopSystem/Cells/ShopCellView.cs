using UnityEngine;

namespace ShopSystem.Cells
{
    [RequireComponent(typeof(Animator))]
    public class ShopCellView : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int HiddenTrigger = Animator.StringToHash(ShopCellAnimator.Triggers.StayHidden);
        private static readonly int RouletteSelectTrigger = Animator.StringToHash(ShopCellAnimator.Triggers.RouletteSelect);
        private static readonly int RouletteDeselectTrigger = Animator.StringToHash(ShopCellAnimator.Triggers.RouletteDeselect);
        private static readonly int ShowTrigger = Animator.StringToHash(ShopCellAnimator.Triggers.Show);

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
            _animator.SetTrigger(RouletteSelectTrigger);
        }
        
        public void RouletteDeselect()
        {
            _animator.SetTrigger(RouletteDeselectTrigger);
        }
    }
}

public static class ShopCellAnimator
{
    public static class Triggers
    {
        public const string Show = nameof(Show);
        public const string StayHidden = nameof(StayHidden);
        public const string RouletteSelect = nameof(RouletteSelect);
        public const string RouletteDeselect = nameof(RouletteDeselect);
    }
}
