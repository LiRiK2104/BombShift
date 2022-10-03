using System.Collections.Generic;
using ShopSystem.Cells;
using ShopSystem.Items;
using UnityEngine;
using UnityEngine.UI;
using ToggleGroup = ShopSystem.Toggles.ToggleGroup;

namespace ShopSystem.Pages
{
    public class ShopPageView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private PageTape _pageTape;
        
        private List<ShopCell> _cells = new List<ShopCell>();

        public void Initialize(ShopPage shopPage, ToggleGroup shopToggleGroup)
        {
            _cells.Clear();
        
            _pageTape.Initialize(shopPage.Name, shopPage.NameTextColor, shopPage.TapeColor);
        
            foreach (var unit in shopPage.Units)
            {
                var cell = Instantiate(shopPage.CellTemplate, _grid.transform);
                cell.Initialize(unit, shopToggleGroup);
                _cells.Add(cell);
                
                shopToggleGroup.AddToggle(cell.Toggle);
            }
        }

        public bool TryGetCell(Skin skin, out ShopCell foundCell)
        {
            foundCell = null;
            
            if (_cells == null || _cells.Count == 0)
                return false;
            
            foreach (var cell in _cells)
            {
                if (cell.ShopUnit.Skin == skin)
                {
                    foundCell = cell;
                    return true;
                }
            }

            return false;
        }
    }
}
