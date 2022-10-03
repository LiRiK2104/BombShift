using ShopSystem.Items;
using UnityEngine;

namespace ShopSystem.Units
{
    [CreateAssetMenu(fileName = "ShopUnitPriced", menuName = "ShopUnit/Priced", order = 51)]
    public class ShopUnitPriced : ShopUnit
    {
        private const int MinCount = 0;
    
        [SerializeField] private Currency _currency;
        [SerializeField] private int _currencyNeedCount;
    
        public Currency Currency => _currency;
        public int CurrencyNeedCount => _currencyNeedCount;

        private void OnValidate()
        {
            _currencyNeedCount = Mathf.Max(MinCount, _currencyNeedCount);
        }
    }
}
