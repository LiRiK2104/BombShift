using Ads;
using UnityEngine;

namespace ShopSystem.InfoBlocks
{
    public class BuyBlock : InfoBlock
    {
        [SerializeField] private BuyButton _buyButton;
        [SerializeField] private RouletteAdsOffer _rouletteAdsOffer;

        private int _price;


        private void OnEnable()
        {
            UpdateButton();
            //TODO: Подключение функции прокрутки
        }

        public override void Initialize(InfoBlockData infoBlockData)
        {
            if (infoBlockData is BuyBlockData buyBlockData)
            {
                _price = buyBlockData.Price;
                UpdateButton();
            }
            
            _rouletteAdsOffer.Initialize();
        }

        private void UpdateButton()
        {
            _buyButton.Initialize(_price);
        }
    }
}
