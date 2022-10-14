using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ads
{
    public class VictoryAdsOffer : AdsOffer
    {
        private const int MinMultiplier = 2;
        private const int MaxMultiplier = 10;
        
        [SerializeField] private Button _skipButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] [Range(MinMultiplier, MaxMultiplier)] private int _multiplier;
        [SerializeField] private TextMeshProUGUI _multiplierTMP;

        private bool _triedWatchAds;
        
        public event Action<AdsMultiplierReward> CompletelyWatched;
        
        
        private void Awake()
        {
            SetMultiplyText();
            OffAllButtons();
        }

        public override void Initialize()
        {
            if (IsInitialized)
                return;

            WatchAdsButton.onClick.AddListener(() =>
            {
                _triedWatchAds = true;
                //if (ads available)
                //  RewardedAdsEnd += OnEndAds;
                //  Watch rewarded ads;
                //else
                //  CloseOffer();

                //temp
                OffAllButtons();
                OnEndAds();
            });
        
            _skipButton.onClick.AddListener(CloseOffer);
            _nextButton.onClick.AddListener(CloseOffer);

            base.Initialize();
        }

        protected override void StartOffer()
        {
            StartCoroutine(OfferProcessing());
        }

        protected override void CancelOffer()
        {
            OffAllButtons();
            _nextButton.gameObject.SetActive(false);
        }

        protected override void OnCompletelyWatched()
        {
            CompletelyWatched?.Invoke(new AdsMultiplierReward(_multiplier));
        }

        private IEnumerator OfferProcessing()
        {
            WatchAdsButton.gameObject.SetActive(true);

            float offerTime = 5;
            yield return new WaitForSeconds(offerTime);
            
            if (_triedWatchAds == false)
                _skipButton.gameObject.SetActive(true);
        }

        private void CloseOffer()
        {
            OffAllButtons(); 
            EndOffer();
        }

        private void OffAllButtons()
        {
            WatchAdsButton.gameObject.SetActive(false);
            _skipButton.gameObject.SetActive(false);
            _nextButton.gameObject.SetActive(false);
        }
        
        private void SetMultiplyText()
        {
            _multiplierTMP.text = $"x{_multiplier.ToString()}";
        }
    }

    public class AdsMultiplierReward
    {
        public int Multiplier { get; }

        public AdsMultiplierReward(int multiplier)
        {
            Multiplier = multiplier;
        }
    }
}
