using Newtonsoft.Json;
using Shop.Items;
using UnityEngine;

namespace Shop
{
    public class CurrencyContainer
    {
        private const int MinCount = 0;
    
        [JsonProperty("currency_id")]
        [JsonConverter(typeof(ItemConverter))]
        public Currency Currency { get; }
        public int Count { get; private set; }

        public CurrencyContainer(Currency currency, int count)
        {
            Currency = currency;
            Count = count;
            ClampCount();
        }

        public void Add(int count)
        {
            Count += count;
            ClampCount();
        }

        public void Remove(int count)
        {
            Count -= count;
            ClampCount();
        }

        private void ClampCount()
        {
            Count = Mathf.Max(MinCount, Count);
        }
    }
}
