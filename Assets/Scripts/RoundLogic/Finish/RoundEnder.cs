using System.Collections;
using RoundLogic.Finish.Victory;
using UnityEngine;

namespace RoundLogic.Finish
{
    public class RoundEnder : MonoBehaviour
    {
        [SerializeField] private VictoryMenu _victoryMenu;

        public void Win(RewardedChestPreset rewardedChestPreset)
        {
            StartCoroutine(OpenVictoryMenu(rewardedChestPreset));
        }

        private IEnumerator OpenVictoryMenu(RewardedChestPreset rewardedChestPreset)
        {
            float delay = 3;
            yield return new WaitForSeconds(delay);
            
            _victoryMenu.Initialize(rewardedChestPreset);
            _victoryMenu.gameObject.SetActive(true);
        }
    }
}
