using Ads;
using ShopSystem;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace RoundLogic.Finish.Victory
{
    public class RewardCreditor : MonoBehaviour
    {
        [Inject] private Inventory _inventory;
        
        private Currency _currency;
        private int _count;
        private VictoryAdsOffer _adsOffer;


        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        
        public void Initialize(Currency currency, int count, VictoryAdsOffer adsOffer)
        {
            _currency = currency;
            _count = count;
            _adsOffer = adsOffer;
        }

        private void Subscribe()
        {
            if (_adsOffer == null)
                return;
            
            _adsOffer.CompletelyWatched += MultiplyReward;
            _adsOffer.Ended += CreditReward;
        }

        private void Unsubscribe()
        {
            if (_adsOffer == null)
                return;
            
            _adsOffer.CompletelyWatched -= MultiplyReward;
            _adsOffer.Ended -= CreditReward;
        }
        
        private void MultiplyReward(AdsMultiplierReward adsMultiplierReward)
        {
            _count *= adsMultiplierReward.Multiplier;
        }

        private void CreditReward()
        {
            _inventory.Add(_currency, _count);
        }
    }
}
