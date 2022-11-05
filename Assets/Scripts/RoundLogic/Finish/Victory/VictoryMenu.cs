using System.Collections;
using Ads;
using Chests;
using SceneManagement;
using ShopSystem;
using ShopSystem.Items;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoundLogic.Finish.Victory
{
    [RequireComponent(typeof(Animator))]
    public class VictoryMenu : MonoBehaviour
    {
        private static readonly int OpenChestTrigger = Animator.StringToHash(VictoryMenuAnimator.Triggers.OpenChest);

        [SerializeField] private RewardCreditor _rewardCreditor;
        [SerializeField] private ChestRewardDemonstrator _rewardDemonstrator;
        [SerializeField] private Button _openChestButton;
        [SerializeField] private VictoryAdsOffer _victoryAdsOffer;

        [Inject] private Shop _shop;
        [Inject] private Inventory _inventory;
        [Inject] private SceneLoader _sceneLoader;

        private Animator _animator;
        private bool _chestOpened;
        private RewardValidator _rewardValidator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _openChestButton.onClick.AddListener(OpenChest);
        }

        private void OnEnable()
        {
            _victoryAdsOffer.Ended += Exit;
        }

        private void OnDisable()
        {
            _victoryAdsOffer.Ended -= Exit;
        }

        public void Initialize(RewardedChestPreset rewardedChestPreset)
        {
            _rewardValidator ??= new RewardValidator(_shop, _inventory);
            ChestReward reward;
            
            do
            {
                reward = rewardedChestPreset.RewardsSet.GetRandomReward();
            } 
            while (_rewardValidator.RewardIsValid(reward) == false);
                
            Currency currency = reward.GetCurrency();
            int count = reward.Count;
            
            _rewardCreditor.Initialize(currency, count, _victoryAdsOffer);
            _rewardDemonstrator.Initialize(currency, count, rewardedChestPreset.Chest, _victoryAdsOffer);
        }

        private void OpenChest()
        {
            if (_chestOpened)
                return;
        
            _animator.SetTrigger(OpenChestTrigger);
            _rewardDemonstrator.Open();

            StartCoroutine(WaitToShowAdsOffer());

            _chestOpened = true;
        }

        private IEnumerator WaitToShowAdsOffer()
        {
            float chestOpeningTime = 3;
        
            yield return new WaitForSeconds(chestOpeningTime);
            _victoryAdsOffer.Initialize();
            _victoryAdsOffer.Ended += Exit;
        }

        private void Exit()
        {
            _victoryAdsOffer.Ended -= Exit;
            StartCoroutine(ExitProcessing());
        }

        private IEnumerator ExitProcessing()
        {
            float delay = 2;
            yield return new WaitForSeconds(delay);
            
            
            //gameObject.SetActive(false);
            _sceneLoader.ReloadScene();
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