using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace ShopSystem.InfoBlocks
{
    public class SliderBlock : InfoBlock
    {
        [SerializeField] private CustomSlider _customSlider;
        
        [Inject] private Inventory _inventory;

        private Currency _currency;
        private int _currencyNeedCount;
        
        
        private void OnEnable()
        {
            UpdateSlider();
        }

        public override void UpdateInfo(InfoBlockData infoBlockData)
        {
            if (infoBlockData is SliderBlockData sliderBlockData)
            {
                _currency = sliderBlockData.Currency;
                _currencyNeedCount = sliderBlockData.CurrencyNeedCount;
                UpdateSlider();
            }
        }

        private void UpdateSlider()
        {
            _inventory.TryGetCurrencyCount(_currency, out int fragmentCollectedCount);
            _customSlider.Initialize(_currencyNeedCount, fragmentCollectedCount);
            //TODO: Установить превью фрагмента
        }
    }
}
