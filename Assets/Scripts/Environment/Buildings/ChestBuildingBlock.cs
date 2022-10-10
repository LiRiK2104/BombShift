using Chests;
using DataBaseSystem;
using RoundLogic.Finish;
using RoundLogic.Finish.Victory;
using UnityEngine;
using Zenject;

namespace Environment.Buildings
{
    public class ChestBuildingBlock : BuildingBlock
    {
        [SerializeField] private RewardLevel _rewardLevel;
        [SerializeField] private ChestCreator _chestCreator;

        [Inject] private DataBase _dataBase;
        [Inject] private RoundEnder _roundEnder;

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
            
            _roundEnder.Win(_rewardedChestPreset);
            //TODO: Воспроизвести эффект
            
            base.DestructSelf();
        }
    }
}
