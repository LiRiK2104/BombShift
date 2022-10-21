using ShopSystem.InfoBlocks;
using ShopSystem.Items;
using UnityEngine;

namespace ShopSystem.Units
{
    [CreateAssetMenu(fileName = nameof(UnitPriced), menuName = "ShopUnit/Priced", order = 51)]
    public class UnitPriced : Unit, IInfoBlockOwner
    {
        private const int MinCount = 0;
    
        [SerializeField] private Currency _currency;
        [SerializeField] private int _currencyNeedCount;
        [SerializeField] private SliderBlock _sliderBlock;

        private SliderBlockData _sliderBlockData;
    
        public Currency Currency => _currency;
        public int CurrencyNeedCount => _currencyNeedCount;
        public InfoBlock InfoBlockPrefab => _sliderBlock;

        public InfoBlockData InfoBlockData
        {
            get
            {
                if (_sliderBlockData == null)
                    _sliderBlockData = new SliderBlockData(_currency, _currencyNeedCount);

                return _sliderBlockData;
            }
        }


        private void OnValidate()
        {
            _currencyNeedCount = Mathf.Max(MinCount, _currencyNeedCount);
        }
    }
}
