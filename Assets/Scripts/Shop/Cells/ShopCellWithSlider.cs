using Shop.Cells.States;
using Shop.Units;
using UnityEngine;

namespace Shop.Cells
{
    public class ShopCellWithSlider : ShopCell
    {
        [SerializeField] private LockedFragmentState _lockedFragmentState;

        protected override LockedState LockedState => _lockedFragmentState;

        public override void Initialize(ShopUnit shopUnit)
        {
            base.Initialize(shopUnit);
            Initialize(shopUnit as ShopUnitPriced);
        }

        private void Initialize(ShopUnitPriced shopUnit)
        {
            ItemStorage.Instance.TryGetCurrencyCount(shopUnit.Currency, out int fragmentCollectedCount);
                
            _lockedFragmentState.SetFragmentIcon(shopUnit.Currency.Icon);
            _lockedFragmentState.Slider.Initialize(shopUnit.CurrencyNeedCount, fragmentCollectedCount);
        }
    }
}
