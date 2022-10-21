using FX;
using ShopSystem.Items;
using UnityEngine;

namespace ShopSystem.InfoBlocks
{
    public abstract class InfoBlock : MonoBehaviour, IWasCreatedFrom<InfoBlock>
    {
        public InfoBlock Prefab { get; private set; }
        
        
        public void OnCreate(InfoBlock prefab)
        {
            Prefab = prefab;
        }

        public abstract void Initialize(InfoBlockData infoBlockData);
    }

    public interface IInfoBlockOwner
    {
        public InfoBlock InfoBlockPrefab { get; }

        public InfoBlockData InfoBlockData { get; }
    }

    public abstract class InfoBlockData { }

    public class BuyBlockData : InfoBlockData
    {
        public int Price { get; }

        public BuyBlockData(int price)
        {
            Price = price;
        }
    }
    
    public class SliderBlockData : InfoBlockData
    {
        public Currency Currency { get; }
        public int CurrencyNeedCount { get; }

        public SliderBlockData(Currency currency, int currencyNeedCount)
        {
            Currency = currency;
            CurrencyNeedCount = currencyNeedCount;
        }
    }
}
