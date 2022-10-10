using Chests;
using UnityEngine;

namespace RoundLogic.Finish.Victory
{
    [CreateAssetMenu(menuName = "RewardedChestPreset", fileName = "RewardedChestPreset", order = 51)]
    public class RewardedChestPreset : ScriptableObject
    {
        [SerializeField] private Chest _chest;
        [SerializeField] private ChestRewardsSet _rewardsSet;

        public Chest Chest => _chest;
        public ChestRewardsSet RewardsSet => _rewardsSet;
    }
}
