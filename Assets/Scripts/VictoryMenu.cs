using System.Collections;
using Chests;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class VictoryMenu : MonoBehaviour
{
    private static readonly int OpenChestTrigger = Animator.StringToHash(VictoryMenuAnimator.Triggers.OpenChest);
    
    [SerializeField] private ChestRewardDemonstartor _rewardDemonstartor;
    [SerializeField] private Button _openChestButton;
    [SerializeField] private AdsOffer _adsOffer;

    private Animator _animator;
    private bool _chestOpened;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _openChestButton.onClick.AddListener(OpenChest);
    }

    private void OpenChest()
    {
        if (_chestOpened)
            return;
        
        _animator.SetTrigger(OpenChestTrigger);
        _rewardDemonstartor.Open();

        StartCoroutine(WaitToShowAdsOffer());

        _chestOpened = true;
    }

    private IEnumerator WaitToShowAdsOffer()
    {
        float chestOpeningTime = 3;
        
        yield return new WaitForSeconds(chestOpeningTime);
        _adsOffer.Initialize(Exit);
    }

    private void Exit()
    {
        gameObject.SetActive(false);
    }
}

public static class VictoryMenuAnimator
{
    public static class Triggers
    {
        public const string OpenChest = "OpenChest";
    }
}
