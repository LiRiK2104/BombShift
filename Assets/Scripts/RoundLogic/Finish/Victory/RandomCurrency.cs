using System.Collections.Generic;
using ShopSystem.Items;
using UnityEngine;

namespace RoundLogic.Finish.Victory
{
    [CreateAssetMenu(menuName = "RandomCurrency", fileName = "RandomCurrency", order = 51)]
    public class RandomCurrency : ScriptableObject
    {
        [SerializeField] private List<Currency> _possibleCurrency;

        public Currency GetRandomCurrency()
        {
            int index = Random.Range(0, _possibleCurrency.Count);
            return _possibleCurrency[index];
        }
    }
}
