using System.Collections.Generic;
using Shop.Cells;
using Shop.Units;
using UnityEngine;

namespace Shop.Pages
{
    public abstract class ShopPage : ScriptableObject
    {
        protected const int MaxUnitsCount = 6;
    
        public abstract ShopCell CellTemplate { get; }
        public abstract List<ShopUnit> Units { get; }
    }
}
