using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace RoundLogic.Finish.Victory
{
    [CreateAssetMenu(menuName = "ChestRewardsSet", fileName = "ChestRewardsSet", order = 51)]
    public class ChestRewardsSet : ScriptableObject
    {
        [SerializeField] private List<ProbabilityCell<ChestReward>> _rewards = new List<ProbabilityCell<ChestReward>>();

        private Roulette _roulette;

        
        public ChestReward GetReward()
        {
            ChestReward reward;
            
            do
            {
                reward = GetRandomReward();
            } 
            while (IsPossibleReward(reward) == false);

            return reward;
        }
        
        private bool IsPossibleReward(ChestReward reward)
        {
            switch (reward.GetCurrency())
            {
                /*case Fragment fragment:
                    return Shop.Instance.TryGetUnit(fragment, out ShopUnitPriced shopUnit) &&
                           (Inventory.Instance.TryGetCurrencyCount(fragment, out int hasCurrencyCount) == false ||
                            hasCurrencyCount < shopUnit.CurrencyNeedCount);*/

                default:
                    return true;
            }
        }
    
        private ChestReward GetRandomReward()
        {
            if (_roulette == null)
                InitializeRoulette();
        
            int index = _roulette.Roll();
        
            if (index >= 0 && index < _rewards.Count)
                return _rewards[index].Item;

            return null;
        }
    
        private void InitializeRoulette()
        {
            var probabilities = new Dictionary<int, double>();
        
            for (int i = 0; i < _rewards.Count; i++)
                probabilities.Add(i, _rewards[i].Probability);

            _roulette = new Roulette(probabilities);
        }
    }
}
