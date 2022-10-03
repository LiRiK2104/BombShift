using EndGame.Victory;
using Helpers;
using UnityEngine;

namespace EndGame
{
    public class Judge : Singleton<Judge>
    {
        [SerializeField] private VictoryMenu _victoryMenu;

        public void Win(RewardedChestPreset rewardedChestPreset)
        {
            _victoryMenu.Initialize(rewardedChestPreset);
            _victoryMenu.gameObject.SetActive(true);
        }
    }
}
