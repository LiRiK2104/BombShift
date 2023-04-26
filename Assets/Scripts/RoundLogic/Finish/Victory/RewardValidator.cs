using ShopSystem;
using ShopSystem.Items;
using ShopSystem.Units;
using Zenject;

namespace RoundLogic.Finish.Victory
{
    public class RewardValidator
    {
        private Shop _shop;
        private Inventory _inventory;
        

        [Inject]
        public RewardValidator(Shop shop, Inventory inventory)
        {
            _shop = shop;
            _inventory = inventory;
        }

        public bool RewardIsValid(ChestReward reward)
        {
            if (reward == null)
                return false;
            
            switch (reward.GetCurrency())
            {
                case Fragment fragment:
                    return _shop.TryGetUnit(fragment, out UnitPriced shopUnit) &&
                           (_inventory.TryGetCurrencyCount(fragment, out int hasCurrencyCount) == false ||
                            hasCurrencyCount < shopUnit.CurrencyNeedCount);

                default:
                    return true;
            }
        }
    }
}
