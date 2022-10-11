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
        [SerializeField] private List<ShopPage> _pages;

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

        public void Buy(ShopUnitIdle shopUnit)
        {
            BuySkin(shopUnit.Skin);
        }
        
        public void Buy(ShopUnitPriced shopUnit)
        {
            if (_inventory.TryGetCurrencyCount(shopUnit.Currency, out int hasCount) && 
                hasCount >= shopUnit.CurrencyNeedCount)
            {
                _inventory.Remove(shopUnit.Currency, shopUnit.CurrencyNeedCount);
                BuySkin(shopUnit.Skin);
            }
        }
        
        public bool TryGetUnit(Fragment fragment, out ShopUnitPriced pricedUnit)
        {
            pricedUnit = null;
            
            foreach (var page in _pages)
            {
                if (page is ShopPagePriced)
                {
                    foreach (var units in page.Units)
                    {
                        if (units is ShopUnitPriced foundUnitPriced && foundUnitPriced.Currency == fragment)
                        {
                            pricedUnit = foundUnitPriced;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void OnSelectedCell(ShopCell shopCell)
        {
            Selected?.Invoke(shopCell.ShopUnit.Skin);
        }

        private void BuySkin(Skin skin)
        {
            _inventory.Add(skin);
            //TODO: Добавить в очередь баннер о новом скине 
        }
    }
}
