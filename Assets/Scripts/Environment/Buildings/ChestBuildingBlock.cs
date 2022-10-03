using System;
using Chests;
using DataBase;
using EndGame;
using EndGame.Victory;
using UnityEngine;

namespace Environment.Buildings
{
    public class ChestBuildingBlock : BuildingBlock
    {
        [SerializeField] private RewardLevel _rewardLevel;
        [SerializeField] private ChestCreator _chestCreator;

        private RewardedChestPreset _rewardedChestPreset =>
            ItemsDataBase.Instance.Core.GetRewardedChestPreset(_rewardLevel);
        
        private void Start()
        {
            if (_rewardedChestPreset == null)
                return;
            
            _chestCreator.CreateChest(_rewardedChestPreset.Chest);
        }

        public override void DestructSelf()
        {
            if (_rewardedChestPreset == null)
                return;
            
            Judge.Instance.Win(_rewardedChestPreset);
            //TODO: Воспроизвести эффект
            
            base.DestructSelf();
        }
    }
}
