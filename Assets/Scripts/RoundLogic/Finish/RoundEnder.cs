using RoundLogic.Finish.Victory;
using UnityEngine;

namespace RoundLogic.Finish
{
    public class RoundEnder : MonoBehaviour
    {
        [SerializeField] private VictoryMenu _victoryMenu;

        public void Win(RewardedChestPreset rewardedChestPreset)
        {
            _victoryMenu.Initialize(rewardedChestPreset);
            _victoryMenu.gameObject.SetActive(true);
        }
    }
}
