using System.Collections.Generic;
using Shop.Cells;
using Shop.Units;
using UnityEngine;

namespace Shop.Pages
{
    [CreateAssetMenu(fileName = "ShopPagePriced", menuName = "ShopPage/Priced", order = 51)]
    public class ShopPagePriced : ShopPage
    {
        [SerializeField] private ShopCellWithSlider _cellTemplate;
        [SerializeField] private List<ShopUnitPriced> _shopUnits = new List<ShopUnitPriced>(MaxUnitsCount);
    
        public override ShopCell CellTemplate => _cellTemplate;
        public override List<ShopUnit> Units => new List<ShopUnit>(_shopUnits);
    }
}
