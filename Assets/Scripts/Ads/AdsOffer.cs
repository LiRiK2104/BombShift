using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ads
{
    public class AdsOffer : MonoBehaviour
    {
        //TODO: Подключить рекламу
        
        private const int MinMultiplier = 2;
        private const int MaxMultiplier = 10;

        [SerializeField] private Button _watchAdsButton;
        [SerializeField] private Button _skipButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] [Range(MinMultiplier, MaxMultiplier)] private int _multiplier;
        [SerializeField] private TextMeshProUGUI _multiplierTMP;

        private bool _initialized;
        private bool _triedWatchAds;

        public event Action<AdsRewardData> CompletelyWatched;
        public event Action CanceledWatch;
        private event Action Initialized;

        private void Awake()
        {
            SetMultiplyText();
            OffAllButtons();
        }

        private void OnDisable()
        {
            Initialized -= StartOffer;
            //RewardedAdsEnd -= OnEndAds;
        }

        private void Start()
        {
            if (_initialized)
                StartOffer();
            else
                Initialized += StartOffer;
        }

        public void Initialize(Action callback)
        {
            if (_initialized || callback == null)
                return;
        
            //callback == closeWinnerMenu 
        
            _watchAdsButton.onClick.AddListener(() =>
            {
                _triedWatchAds = true;
                //if (ads available)
                //  RewardedAdsEnd += OnEndAds;
                //  Watch rewarded ads;
                //else
                //  callback?.Invoke();

                //temp
                OnEndAds(callback);
            });
        
            _skipButton.onClick.AddListener(() =>
            {
                OffAllButtons(); 
                callback?.Invoke();
            });
            
            _nextButton.onClick.AddListener(() =>
            {
                OffAllButtons(); 
                callback?.Invoke();
            });

            _initialized = true;
            Initialized?.Invoke();
        }

        private void StartOffer()
        {
            Initialized -= StartOffer;

            //if (ads available)
            if (true)
                StartCoroutine(OfferProcessing());
            else
                _nextButton.gameObject.SetActive(true);
        }

        private IEnumerator OfferProcessing()
        {
            _watchAdsButton.gameObject.SetActive(true);

            float offerTime = 5;
            yield return new WaitForSeconds(offerTime);
            
            if (_triedWatchAds == false)
                _skipButton.gameObject.SetActive(true);
        }

        private void OnEndAds(Action callback)
        {
            //RewardedAdsEnd -= OnEndAds;
            OffAllButtons();
            
            if (true)
                CompletelyWatched?.Invoke(new AdsRewardData(_multiplier));
            
            callback?.Invoke();
        }

        private void CancelWatchAds()
        {
            CanceledWatch?.Invoke();
        }

        private void OffAllButtons()
        {
            _watchAdsButton.gameObject.SetActive(false);
            _skipButton.gameObject.SetActive(false);
            _nextButton.gameObject.SetActive(false);
        }
        
        private void SetMultiplyText()
        {
            _multiplierTMP.text = $"x{_multiplier.ToString()}";
        }
    }

    public class AdsRewardData
    {
        public int Multiplier { get; }

        public AdsRewardData(int multiplier)
        {
            Multiplier = multiplier;
        }
    }
}
