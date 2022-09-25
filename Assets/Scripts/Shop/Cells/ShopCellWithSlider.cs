using Shop.Cells.States;
using Shop.Items;
using Shop.Toggles;
using Shop.Units;
using UnityEngine;

namespace Shop.Cells
{
    public class ShopCellWithSlider : ShopCell
    {
        [SerializeField] private LockedFragmentState _lockedFragmentState;

        protected override LockedState LockedState => _lockedFragmentState;

        public override void Initialize(ShopUnit shopUnit, ToggleGroup shopPageToggleGroup)
        {
            base.Initialize(shopUnit, shopPageToggleGroup);
            Initialize(shopUnit as ShopUnitPriced);
        }

        private void Initialize(ShopUnitPriced shopUnit)
        {
            Inventory.Instance.TryGetCurrencyCount(shopUnit.Currency, out int fragmentCollectedCount);
                
            _lockedFragmentState.SetPreview(shopUnit.Currency as Fragment);
            _lockedFragmentState.Slider.Initialize(shopUnit.CurrencyNeedCount, fragmentCollectedCount);
        }
    }
}
