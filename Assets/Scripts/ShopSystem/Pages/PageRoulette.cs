using System.Collections.Generic;
using ShopSystem.Cells;
using ShopSystem.InfoBlocks;
using ShopSystem.Units;
using UnityEngine;

namespace ShopSystem.Pages
{
    [CreateAssetMenu(fileName = nameof(PageRoulette), menuName = "ShopPage/Idle", order = 51)]
    public class PageRoulette : Page, IInfoBlockOwner
    {
        private const int MinPrice = 100;
        private const int MaxPrice = 1500;
        
        [SerializeField] [Range(MinPrice, MaxPrice)] private int _price;
        [SerializeField] private CellIdle _cellTemplate;
        [SerializeField] private List<UnitIdle> _shopUnits = new List<UnitIdle>(MaxUnitsCount);
        [SerializeField] private BuyBlock _buyBlockPrefab;
        
        private BuyBlockData _buyBlockData;

        public override Cell CellTemplate => _cellTemplate;
        public override List<Unit> Units => new List<Unit>(_shopUnits);
        public InfoBlock InfoBlockPrefab => _buyBlockPrefab;

        public InfoBlockData InfoBlockData
        {
            get
            {
                if (_buyBlockData == null)
                    _buyBlockData = new BuyBlockData(_price);

                return _buyBlockData;
            }
        }
    }
}
