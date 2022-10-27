using System.Collections.Generic;
using ShopSystem.Cells;
using ShopSystem.Units;
using UnityEngine;

namespace ShopSystem.Pages
{
    [CreateAssetMenu(fileName = nameof(PagePriced), menuName = "ShopPage/Priced", order = 51)]
    public class PagePriced : Page
    {
        [SerializeField] private PageView _pageView;
        [SerializeField] private CellWithSlider _cellTemplate;
        [SerializeField] private List<UnitPriced> _shopUnits = new List<UnitPriced>(MaxUnitsCount);
    
        public override PageView PageViewTemplate => _pageView;
        public override Cell CellTemplate => _cellTemplate;
        public override List<Unit> Units => new List<Unit>(_shopUnits);
    }
}
