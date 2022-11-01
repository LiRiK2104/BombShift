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
        private const string GemsDepositKey = "gems_deposit";

        [SerializeField] private List<Page> _pages;

        [Inject] private Inventory _inventory; 
        
        private ShopView _shopView;
        
        public event Action<Skin> Selected;
        public event Action<Skin> Collected;

        public ShopView ShopView
        {
            get
            {
                if (_shopView == null)
                    _shopView = GetComponent<ShopView>();

                return _shopView;
            }
        }

        
        private void Start()
        {
            BuyCollectedSkins();
        }


        public void Initialize()
        {
            TransferGemsDepositToInventory();
            _shopView.Initialize(_pages);
        }

        public void Buy(UnitIdle unit, PageRoulette pageRoulette)
        {
            int inventoryCount = _inventory.GetGemsCount();
            int depositCount = LoadGemsDeposit();

            if (inventoryCount + depositCount >= pageRoulette.Price)
            {
                depositCount -= pageRoulette.Price;

                if (depositCount < 0)
                    _inventory.RemoveGems(Mathf.Abs(depositCount));
                else
                    _inventory.AddGems(depositCount);
                
                SetZeroDeposit();
                BuySkin(unit.Skin);
            }
        }

        public void SetGemsDeposit(int price)
        {
            int hasGems = _inventory.GetGemsCount();
            
            if (hasGems >= price)
            {
                _inventory.RemoveGems(price);
                SaveGemsDeposit(price);
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
        
        private void Buy(UnitPriced unit)
        {
            if (_inventory.TryGetCurrencyCount(unit.Currency, out int hasCount) && hasCount >= unit.CurrencyNeedCount)
            {
                _inventory.Remove(unit.Currency, unit.CurrencyNeedCount);
                BuySkin(unit.Skin);
            }
        }
        
        private void BuyCollectedSkins()
        {
            foreach (var page in _pages)
            {
                foreach (var unit in page.Units)
                {
                    if (unit is UnitPriced unitPriced && 
                        _inventory.HasSkin(unitPriced.Skin) == false && 
                        _inventory.TryGetCurrencyCount(unitPriced.Currency, out int hasCount) && 
                        hasCount >= unitPriced.CurrencyNeedCount)
                    {
                        Buy(unitPriced);
                        Collected?.Invoke(unitPriced.Skin);
                    }
                }
            }
        }
        
        private void SetZeroDeposit()
        {
            SaveGemsDeposit(0);
        }
        
        private void SaveGemsDeposit(int gemsDeposit)
        {
            PlayerPrefs.SetInt(GemsDepositKey, gemsDeposit);
        }

        private int LoadGemsDeposit()
        {
            return PlayerPrefs.GetInt(GemsDepositKey, 0);
        }
        
        private void TransferGemsDepositToInventory()
        {
            int depositGems = LoadGemsDeposit();
            _inventory.AddGems(depositGems);
            SetZeroDeposit();
        }

        private void BuySkin(Skin skin)
        {
            _inventory.Add(skin);
            //TODO: Добавить в очередь баннер о новом скине 
        }
    }
}
