using EndGame.Victory;
using Helpers;
using ShopSystem.Items;
using UnityEngine;

namespace Chests
{
    [RequireComponent(typeof(Animator))]
    public class ChestRewardDemonstartor : MonoBehaviour
    {
        private const string TVLayer = "TV";
        private static readonly int PullOut = Animator.StringToHash(RewardPointAnimator.Triggers.PullOut);
    
        [SerializeField] private Transform _itemPoint;
        [SerializeField] private ChestCreator _chestCreator;
    
        private Animator _animator;
        private Item _itemTemplate;
        private Item _item;
        private bool _isOpened;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Initialize(RewardedChestPreset rewardedChestPreset)
        {
            _itemTemplate = rewardedChestPreset.RewardsSet.GetReward().Currency;
            
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

        private void CreateItem()
        {
            _item = Instantiate(_itemTemplate, _itemPoint.position, _itemPoint.rotation, _itemPoint);
            _item.gameObject.SetLayerToThisAndChildren(LayerMask.NameToLayer(TVLayer));
        }

        private void PullOutItem()
        {
            _animator.SetTrigger(PullOut);
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