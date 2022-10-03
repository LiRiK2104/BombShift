using System;
using ShopSystem.Items;
using UnityEngine;

namespace EndGame.Victory
{
    [Serializable]
    public class ChestReward
    {
        private const int MinCount = 0;

        [SerializeField] private Currency _currency;
        [SerializeField] private int _count;

        public Currency Currency => _currency;
        public int Count => _count;

        private void OnValidate()
        {
            ClampCount();
        }

        private void ClampCount()
        {
            _count = Mathf.Max(MinCount, Count);
        }
    }
}
