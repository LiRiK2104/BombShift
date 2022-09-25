using Shop.Items;
using Shop.Units;
using UnityEngine;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
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

        private void BuySkin(Skin skin)
        {
            Inventory.Instance.Add(skin);
            //TODO: Добавить в очередь баннер о новом скине 
        }
    }
}
