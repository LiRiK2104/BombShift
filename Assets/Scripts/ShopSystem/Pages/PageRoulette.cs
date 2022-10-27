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

        [SerializeField] private RoulettePageView _roulettePageView;
        [SerializeField] [Range(MinPrice, MaxPrice)] private int _price;
        [SerializeField] private CellIdle _cellTemplate;
        [SerializeField] private List<UnitIdle> _shopUnits = new List<UnitIdle>(MaxUnitsCount);
        [SerializeField] private RouletteBlock rouletteBlockPrefab;
        
        private RouletteBlockData _rouletteBlockData;

        public override PageView PageViewTemplate => _roulettePageView;
        public override Cell CellTemplate => _cellTemplate;
        public override List<Unit> Units => new List<Unit>(_shopUnits);
        public InfoBlock InfoBlockPrefab => rouletteBlockPrefab;
        public int Price => _price;

        public InfoBlockData InfoBlockData
        {
            get
            {
                if (_rouletteBlockData == null)
                    _rouletteBlockData = new RouletteBlockData(_price);

                return _rouletteBlockData;
            }
        }
    }
}
