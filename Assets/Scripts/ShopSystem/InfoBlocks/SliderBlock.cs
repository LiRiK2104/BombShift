using ItemSetters;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace ShopSystem.InfoBlocks
{
    [RequireComponent(typeof(Animator))]
    public class SliderBlock : InfoBlock
    {
        private static readonly int ShowTrigger = Animator.StringToHash(SliderBlockAnimator.Triggers.Show);
        
        [SerializeField] private CustomSlider _customSlider;
        [SerializeField] private FragmentSetter _fragmentSetter;
        
        [Inject] private Inventory _inventory;

        private Animator _animator;
        private Currency _currency;
        private int _currencyNeedCount;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            UpdateSlider();
        }

        public override void UpdateInfo(InfoBlockData infoBlockData)
        {
            if (infoBlockData is SliderBlockData sliderBlockData)
            {
                var oldCurrency = _currency;
                
                _currency = sliderBlockData.Currency;
                _currencyNeedCount = sliderBlockData.CurrencyNeedCount;
                UpdateSlider();
                
                if (_currency != oldCurrency && sliderBlockData.Currency is Fragment fragment)
                    SetFragmentPreview(fragment);
            }
        }

        private void UpdateSlider()
        {
            _inventory.TryGetCurrencyCount(_currency, out int fragmentCollectedCount);
            _customSlider.Initialize(_currencyNeedCount, fragmentCollectedCount);
        }

        private void SetFragmentPreview(Fragment fragment)
        {
            _fragmentSetter.SetFragment(fragment);
            _animator.SetTrigger(ShowTrigger);
        }
    }

    public static class SliderBlockAnimator
    {
        public static class Triggers
        {
            public const string Show = nameof(Show);
        }
    } 
}
