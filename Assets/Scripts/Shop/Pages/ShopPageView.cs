using Shop.Items;
using UnityEngine;
using UnityEngine.UI;
using ToggleGroup = Shop.Toggles.ToggleGroup;

namespace Shop.Pages
{
    public class ShopPageView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private PageTape _pageTape;

        private ShopPage _shopPage;

        public void Initialize(ShopPage shopPage, ToggleGroup shopToggleGroup)
        {
            _shopPage = shopPage;
        
            _pageTape.Initialize(shopPage.Name, shopPage.NameTextColor, shopPage.TapeColor);
        
            foreach (var unit in shopPage.Units)
            {
                var cell = Instantiate(shopPage.CellTemplate, _grid.transform);
                cell.Initialize(unit, shopToggleGroup);
                shopToggleGroup.AddToggle(cell.Toggle);
            }
        }

        public bool HasSkin(Skin targetSkin)
        {
            foreach (var unit in _shopPage.Units)
            {
                if (unit.Skin == targetSkin)
                    return true;
            }

            return false;
        }
    }
}
