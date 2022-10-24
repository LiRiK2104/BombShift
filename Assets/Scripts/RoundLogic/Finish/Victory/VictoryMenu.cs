using System.Collections;
using Ads;
using Chests;
using UnityEngine;
using UnityEngine.UI;

namespace RoundLogic.Finish.Victory
{
    [RequireComponent(typeof(Animator))]
    public class VictoryMenu : MonoBehaviour
    {
        private static readonly int OpenChestTrigger = Animator.StringToHash(VictoryMenuAnimator.Triggers.OpenChest);
    
        [SerializeField] private ChestRewardDemonstrator rewardDemonstrator;
        [SerializeField] private Button _openChestButton;
        [SerializeField] private VictoryAdsOffer victoryAdsOffer;

        private Animator _animator;
        private bool _chestOpened;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _openChestButton.onClick.AddListener(OpenChest);
        }

        private void OnEnable()
        {
            victoryAdsOffer.CompletelyWatched += rewardDemonstrator.MultiplyReward;
            victoryAdsOffer.Ended += Exit;
        }

        private void OnDisable()
        {
            victoryAdsOffer.CompletelyWatched -= rewardDemonstrator.MultiplyReward;
            victoryAdsOffer.Ended -= Exit;
        }

        public void Initialize(RewardedChestPreset rewardedChestPreset)
        {
            rewardDemonstrator.Initialize(rewardedChestPreset);
        }

        private void OpenChest()
        {
            if (_chestOpened)
                return;
        
            _animator.SetTrigger(OpenChestTrigger);
            rewardDemonstrator.Open();

            StartCoroutine(WaitToShowAdsOffer());

            _chestOpened = true;
        }

        private IEnumerator WaitToShowAdsOffer()
        {
            float chestOpeningTime = 3;
        
            yield return new WaitForSeconds(chestOpeningTime);
            victoryAdsOffer.Initialize();
            victoryAdsOffer.Ended += Exit;
        }

        private void Exit()
        {
            victoryAdsOffer.Ended -= Exit;
            StartCoroutine(ExitProcessing());
        }

        private IEnumerator ExitProcessing()
        {
            //TODO: Добавить сохранение данных, затухание экрана и перезагрузку сцены.
            
            float delay = 2;
            yield return new WaitForSeconds(delay);
            
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
}