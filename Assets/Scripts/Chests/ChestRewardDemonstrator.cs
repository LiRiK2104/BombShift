using Ads;
using Helpers;
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
    
        private VictoryAdsOffer _adsOffer;
        private Animator _animator;
        private Item _itemTemplate;
        private Item _item;
        private int _itemCount;
        private bool _isOpened;

        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }


        public void Initialize(Currency currency, int count, Chest chest, VictoryAdsOffer adsOffer)
        {
            _itemTemplate = currency;
            _itemCount = count;
            _adsOffer = adsOffer;

            _rewardCountText.TextMeshPro.text = GetFormattedItemCount();
            _rewardCountText.SetIdleState();

            _chestCreator.CreateChest(chest);
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
        
        private void Subscribe()
        {
            if (_adsOffer == null)
                return;
            
            _adsOffer.CompletelyWatched += MultiplyReward;
        }

        private void Unsubscribe()
        {
            if (_adsOffer == null)
                return;
            
            _adsOffer.CompletelyWatched -= MultiplyReward;
        }
        
        private void MultiplyReward(AdsMultiplierReward adsMultiplierReward)
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