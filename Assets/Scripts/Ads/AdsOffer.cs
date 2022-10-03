using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ads
{
    public class AdsOffer : MonoBehaviour
    {
        [SerializeField] private Button _watchAdsButton;
        [SerializeField] private Button _sorryNoButton;

        private bool _initialized;

        private event Action Initialized;

        private void Awake()
        {
            _watchAdsButton.gameObject.SetActive(false);
            _sorryNoButton.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Initialized -= StartOffer;
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
                //Watch rewarded ads;\
                //onRewardedAdsEnd += callback;
            });
        
            _sorryNoButton.onClick.AddListener(() => { callback?.Invoke(); });

            _initialized = true;
            Initialized?.Invoke();
        }

        private void StartOffer()
        {
            Initialized -= StartOffer;
            StartCoroutine(OfferProcessing());
        }

        private IEnumerator OfferProcessing()
        {
            _watchAdsButton.gameObject.SetActive(true);

            float offerTime = 5;
            yield return new WaitForSeconds(offerTime);
            _sorryNoButton.gameObject.SetActive(true);
        }
    
        private void OnAdsWatchedCompletely()
        {
            //give reward
        }
    }
}
