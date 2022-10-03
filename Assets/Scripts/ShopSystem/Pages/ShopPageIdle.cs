using System.Collections.Generic;
using ShopSystem.Cells;
using ShopSystem.Units;
using UnityEngine;

namespace ShopSystem.Pages
{
    [CreateAssetMenu(fileName = "ShopPageIdle", menuName = "ShopPage/Idle", order = 51)]
    public class ShopPageIdle : ShopPage
    {
        [SerializeField] private ShopCellIdle _cellTemplate;
        [SerializeField] private List<ShopUnitIdle> _shopUnits = new List<ShopUnitIdle>(MaxUnitsCount);

        public override ShopCell CellTemplate => _cellTemplate;
        public override List<ShopUnit> Units => new List<ShopUnit>(_shopUnits);
    }
}
