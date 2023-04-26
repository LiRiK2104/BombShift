using ShopSystem.Cells.States;
using ShopSystem.Items;
using ShopSystem.Toggles;
using ShopSystem.Units;
using UnityEngine;

namespace ShopSystem.Cells
{
    public class CellWithSlider : Cell
    {
        [SerializeField] private LockedFragmentState _lockedFragmentState;

        protected override LockedState LockedState => _lockedFragmentState;

        protected override void OnEnable()
        {
            base.OnEnable();
            Inventory.CurrencyAdded += UpdateSlider;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Inventory.CurrencyAdded -= UpdateSlider;
        }

        public override void Initialize(Unit unit, ToggleGroup shopPageToggleGroup)
        {
            base.Initialize(unit, shopPageToggleGroup);
            Initialize(unit as UnitPriced);
        }

        private void Initialize(UnitPriced unit)
        {
            Inventory.TryGetCurrencyCount(unit.Currency, out int fragmentCollectedCount);
                
            _lockedFragmentState.SetPreview(unit.Currency as Fragment);
            _lockedFragmentState.Slider.Initialize(unit.CurrencyNeedCount, fragmentCollectedCount);
        }

        private void UpdateSlider()
        {
            if (Unit is UnitPriced shopUnitPriced && 
                Inventory.TryGetCurrencyCount(shopUnitPriced.Currency, out int hasCount))
            {
                _lockedFragmentState.Slider.SetValue(hasCount);   
            }
        }
    }
}
