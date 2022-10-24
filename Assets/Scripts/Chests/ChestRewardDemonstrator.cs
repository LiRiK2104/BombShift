using Ads;
using Helpers;
using RoundLogic.Finish.Victory;
using ShopSystem.Items;
using UI;
using UnityEngine;

namespace Chests
{
    [RequireComponent(typeof(Animator))]
    public class ChestRewardDemonstrator : MonoBehaviour
    {
        private const string TVLayer = "TV";
        private static readonly int PullOut = Animator.StringToHash(RewardPointAnimator.Triggers.PullOut);
    
        [SerializeField] private Transform _itemPoint;
        [SerializeField] private ChestCreator _chestCreator;
        [SerializeField] private RewardCountText _rewardCountText;
    
        private Animator _animator;
        private Item _itemTemplate;
        private Item _item;
        private int _itemCount;
        private bool _isOpened;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Initialize(RewardedChestPreset rewardedChestPreset)
        {
            ChestReward reward = rewardedChestPreset.RewardsSet.GetReward();
            _itemTemplate = reward.GetCurrency();
            _itemCount = reward.Count;
            _rewardCountText.TextMeshPro.text = GetFormattedItemCount();
            _rewardCountText.SetIdleState();

            _chestCreator.CreateChest(rewardedChestPreset.Chest);
            _chestCreator.Chest.gameObject.SetLayerToThisAndChildren(LayerMask.NameToLayer(TVLayer));
        }

        public void Open()
        {
            if (_isOpened)
                return;
            
            CreateItem();
            _chestCreator.Chest.Open();
            PullOutItem();

            _isOpened = true;
        }
        
        public void MultiplyReward(AdsMultiplierReward adsMultiplierReward)
        {
            _itemCount *= adsMultiplierReward.Multiplier;

            _rewardCountText.TextMeshPro.text = GetFormattedItemCount();
            _rewardCountText.SetMultipliedState();
        }

        private void CreateItem()
        {
            _item = Instantiate(_itemTemplate, _itemPoint.position, _itemPoint.rotation, _itemPoint);
            _item.gameObject.SetLayerToThisAndChildren(LayerMask.NameToLayer(TVLayer));
        }

        private void PullOutItem()
        {
            _animator.SetTrigger(PullOut);
        }

        private string GetFormattedItemCount()
        {
            return $"+{_itemCount}";
        }
    }

    public static class RewardPointAnimator
    {
        public static class Triggers
        {
            public const string PullOut = "PullOut";
        }
    }
}