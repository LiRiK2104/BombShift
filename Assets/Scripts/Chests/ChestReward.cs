using Shop.Items;
using UnityEngine;

namespace Chests
{
    [RequireComponent(typeof(Animator))]
    public class ChestReward : MonoBehaviour
    {
        private static readonly int PullOut = Animator.StringToHash(RewardPointAnimator.Triggers.PullOut);
    
        [SerializeField] private ChestItem _itemTemplate;
        [SerializeField] private Transform _itemPoint;
        [SerializeField] private Light _lightPoint;
        [SerializeField] private ChestCreator _chestCreator;
    
        private Animator _animator;
        private ChestItem _chestItem;
        private Item _item;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            SetColorToLightPont();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Open();
            }
        }

        public void Initialize(Item item)
        {
            _item = item;
        }

        private void Open()
        {
            CreateItem();
            _chestCreator.Chest.Open();
            PullOutItem();
        }

        private void SetColorToLightPont()
        {
            _lightPoint.color = _chestCreator.Chest.LightColor;
        }

        private void CreateItem()
        {
            _chestItem = Instantiate(_itemTemplate, _itemPoint.position, _itemPoint.rotation, _itemPoint);
            _chestItem.Initialize(_item);
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