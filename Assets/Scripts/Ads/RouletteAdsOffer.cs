using System;
using ShopSystem.Items;
using TMPro;
using UnityEngine;

namespace Ads
{
    public class RouletteAdsOffer : AdsOffer
    {
        private const int MinRewardCount = 100;
        private const int MaxRewardCount = 1500;
        
        [SerializeField] private Gem _reward;
        [SerializeField] [Range(MinRewardCount, MaxRewardCount)] private int _rewardCount;
        [SerializeField] private TextMeshProUGUI _rewardCountTMP;
        
        public event Action<AdsCurrencyReward> CompletelyWatched;
        
        
        private void Awake()
        {
            SetRewardText();
        }
        
        public override void Initialize()
        {
            if (IsInitialized)
                return;

            WatchAdsButton.onClick.AddListener(() =>
            {
                //if (ads available)
                //  RewardedAdsEnd += OnEndAds;
                //  Watch rewarded ads;
                //else
                //  callback?.Invoke();

                //temp
                OnEndAds();
            });

            base.Initialize();
        }
        
        protected override void StartOffer()
        {
            WatchAdsButton.gameObject.SetActive(false);
        }

        protected override void CancelOffer()
        {
            WatchAdsButton.gameObject.SetActive(false);
        }

        protected override void OnCompletelyWatched()
        {
            CompletelyWatched?.Invoke(new AdsCurrencyReward(_reward, _rewardCount));
        }
        
        private void SetRewardText()
        {
            _rewardCountTMP.text = _rewardCount.ToString();
        }
    }
    
    public class AdsCurrencyReward
    {
        public Gem Gem { get; }
        public int Count { get; }

        public AdsCurrencyReward(Gem gem, int count)
        {
            Count = count;
            Gem = gem;
        }
    }
}
