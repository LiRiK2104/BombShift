using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ads
{
    public abstract class AdsOffer : MonoBehaviour
    {
        //TODO: Подключить рекламу

        [SerializeField] protected Button WatchAdsButton;
        
        protected bool IsInitialized;

        public event Action Ended;
        protected event Action Initialized;
        
        
        private void OnEnable()
        {
            if (IsInitialized)
                TryStartOffer();
            else
                Initialized += TryStartOffer;
        }
        
        private void OnDisable()
        {
            Initialized -= TryStartOffer;
            //RewardedAdsEnd -= OnEndAds;
        }

        public virtual void Initialize()
        {
            if (IsInitialized)
                return;
            
            IsInitialized = true;
            Initialized?.Invoke();
        }

        protected abstract void StartOffer();
        protected abstract void CancelOffer();
        protected abstract void OnCompletelyWatched();

        protected void OnEndAds()
        {
            //RewardedAdsEnd -= OnEndAds;

            if (true)
                OnCompletelyWatched();
            
            EndOffer();
        }

        protected void EndOffer()
        {
            Ended?.Invoke();
        }

        private void TryStartOffer()
        {
            Initialized -= TryStartOffer;

            //if (ads available)
            if (true)
                StartOffer();
            else
                CancelOffer();
        }
    }
}
