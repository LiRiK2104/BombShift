using System;
using System.Collections.Generic;
using ShopSystem.Cells;
using ShopSystem.Items;
using ShopSystem.Pages;
using ShopSystem.Units;
using UnityEngine;
using Zenject;

namespace ShopSystem
{
    [RequireComponent(typeof(ShopView))]
    public class Shop : MonoBehaviour, IInitializable
    {
        [SerializeField] private List<Page> _pages;

        [Inject] private Inventory _inventory; 
        
        private ShopView _shopView;
        
        public event Action<Skin> Selected;

        public ShopView ShopView
        {
            get
            {
                if (_shopView == null)
                    _shopView = GetComponent<ShopView>();

                return _shopView;
            }
        }
        
        
        public void Initialize()
        {
            _shopView.Initialize(_pages);
        }

        public void Buy(UnitIdle unit)
        {
            BuySkin(unit.Skin);
        }
        
        public void Buy(UnitPriced unit)
        {
            if (_inventory.TryGetCurrencyCount(unit.Currency, out int hasCount) && 
                hasCount >= unit.CurrencyNeedCount)
            {
                _inventory.Remove(unit.Currency, unit.CurrencyNeedCount);
                BuySkin(unit.Skin);
            }
        }
        
        public bool TryGetUnit(Fragment fragment, out UnitPriced pricedUnit)
        {
            pricedUnit = null;
            
            foreach (var page in _pages)
            {
                if (page is PagePriced)
                {
                    foreach (var units in page.Units)
                    {
                        if (units is UnitPriced foundUnitPriced && foundUnitPriced.Currency == fragment)
                        {
                            pricedUnit = foundUnitPriced;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void OnSelectedCell(Cell cell)
        {
            Selected?.Invoke(cell.Unit.Skin);
        }

        private void BuySkin(Skin skin)
        {
            _inventory.Add(skin);
            //TODO: Добавить в очередь баннер о новом скине 
        }
    }
}
