using System.Collections.Generic;
using Helpers;
using RoundLogic.Finish.Victory;
using ShopSystem.Items;
using UnityEngine;

namespace DataBaseSystem
{
    [CreateAssetMenu(menuName = "DataBase", fileName = "DataBase", order = 67)]
    public class DataBaseCore : ScriptableObject
    {
        [SerializeField] private List<Item> _items = new List<Item>();

        [SerializeField] private UnityDictionary<RewardLevel, RewardedChestPreset> _rewardedChestsPresets =
            new UnityDictionary<RewardLevel, RewardedChestPreset>();

        [SerializeField] private Skin _defaultSkin;

        public Skin DefaultSkin => _defaultSkin;
        
        
        public bool TryGetItem(int id, out Item foundItem)
        {
            foundItem = null;

            foreach (var item in _items)
            {
                if (item.Id == id)
                {
                    foundItem = item;
                    return true;
                }
            }

            return false;
        }

        public RewardedChestPreset GetRewardedChestPreset(RewardLevel rewardLevel)
        {
            _rewardedChestsPresets.TryGetValue(rewardLevel, out RewardedChestPreset rewardedChestPreset);
            return rewardedChestPreset;
        }
    }

    public enum RewardLevel
    {
        First,
        Second,
        Third
    }
}
