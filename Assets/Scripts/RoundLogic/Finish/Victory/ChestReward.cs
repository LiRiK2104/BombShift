using System;
using ShopSystem.Items;
using UnityEngine;

namespace RoundLogic.Finish.Victory
{
    [Serializable]
    public class ChestReward
    {
        private const int MinCount = 0;

        [SerializeField] private RandomCurrency _randomCurrency;
        [SerializeField] private int _count;
        
        public int Count => _count;

        private void OnValidate()
        {
            ClampCount();
        }

        public Currency GetCurrency()
        {
            return _randomCurrency.GetRandomCurrency();
        }

        private void ClampCount()
        {
            _count = Mathf.Max(MinCount, Count);
        }
    }
}
