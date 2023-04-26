using System;
using Ads;
using UnityEngine;
using Zenject;

namespace ShopSystem.InfoBlocks
{
    public class RouletteBlock : InfoBlock
    {
        [SerializeField] private BuyButton _buyButton;
        [SerializeField] private RouletteAdsOffer _rouletteAdsOffer;

        [Inject] private Shop _shop;
        
        private int _price;
        private Action _rollFunction;


        private void OnEnable()
        {
            UpdateButton();
        }

        public override void UpdateInfo(InfoBlockData infoBlockData)
        {
            if (infoBlockData is RouletteBlockData rouletteBlockData)
            {
                _price = rouletteBlockData.Price;
                
                _rollFunction = () =>
                {
                    _shop.SetGemsDeposit(_price);
                    _shop.ShopView.RouletteStarter.Roll();
                };
                
                UpdateButton();
            }
            
            _rouletteAdsOffer.Initialize();
        }

        private void UpdateButton()
        {
            _buyButton.UpdateInfo(_price, _rollFunction);
        }
    }
}
