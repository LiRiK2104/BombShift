using System.Collections.Generic;
using Helpers;
using ShopSystem.Items;
using ShopSystem.Pages;
using ShopSystem.Units;
using UnityEngine;

namespace ShopSystem
{
    [RequireComponent(typeof(Shop))]
    public class Shop : Singleton<Shop>
    {
        [SerializeField] private List<ShopPage> _pages;
        
        private ShopView _shopView;

        public ShopView ShopView => _shopView;
        
        private void Awake()
        {
            _shopView = GetComponent<ShopView>();
            _shopView.Initialize(_pages);
        }

        public void Buy(ShopUnitIdle shopUnit)
        {
            BuySkin(shopUnit.Skin);
        }
        
        public void Buy(ShopUnitPriced shopUnit)
        {
            if (Inventory.Instance.TryGetCurrencyCount(shopUnit.Currency, out int hasCount) && 
                hasCount >= shopUnit.CurrencyNeedCount)
            {
                Inventory.Instance.Remove(shopUnit.Currency, shopUnit.CurrencyNeedCount);
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

        private void BuySkin(Skin skin)
        {
            Inventory.Instance.Add(skin);
            //TODO: Добавить в очередь баннер о новом скине 
        }
    }
}
