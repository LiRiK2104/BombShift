using Chests;
using DataBaseSystem;
using EndGame;
using EndGame.Victory;
using UnityEngine;
using Zenject;

namespace Environment.Buildings
{
    public class ChestBuildingBlock : BuildingBlock
    {
        [SerializeField] private RewardLevel _rewardLevel;
        [SerializeField] private ChestCreator _chestCreator;

        [Inject] private DataBase _dataBase;

        private RewardedChestPreset _rewardedChestPreset => _dataBase.Core.GetRewardedChestPreset(_rewardLevel);
        
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
