using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdsOffer : MonoBehaviour
{
    [SerializeField] private Button _watchAdsButton;
    [SerializeField] private Button _sorryNoButton;

    private bool _initialized;

    private event Action OnInitialized;

    private void Awake()
    {
        _watchAdsButton.gameObject.SetActive(false);
        _sorryNoButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        OnInitialized -= StartOffer;
    }

    private void Start()
    {
        //temp
        Initialize(delegate {  });
        
        if (_initialized)
            StartOffer();
        else
            OnInitialized += StartOffer;
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
        OnInitialized?.Invoke();
    }

    private void StartOffer()
    {
        OnInitialized -= StartOffer;
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
