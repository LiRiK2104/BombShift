using ShopSystem;
using ShopSystem.Items;
using TMPro;
using UnityEngine;
using Zenject;

namespace Ads
{
    public class RouletteAdsOffer : AdsOffer
    {
        private const int MinRewardCount = 100;
        private const int MaxRewardCount = 1500;
        
        [SerializeField] private Gem _reward;
        [SerializeField] [Range(MinRewardCount, MaxRewardCount)] private int _rewardCount;
        [SerializeField] private TextMeshProUGUI _rewardCountTMP;

        [Inject] private Inventory _inventory;


        private void Awake()
        {
            SetRewardText();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            WatchAdsButton.onClick.RemoveAllListeners();
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
            WatchAdsButton.gameObject.SetActive(true);
        }

        protected override void CancelOffer()
        {
            WatchAdsButton.gameObject.SetActive(false);
        }

        protected override void OnCompletelyWatched()
        {
            _inventory.Add(_reward, _rewardCount);
        }
        
        private void SetRewardText()
        {
            _rewardCountTMP.text = $"+{_rewardCount.ToString()}";
        }
    }
}
