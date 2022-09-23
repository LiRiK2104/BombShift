using Shop;
using Shop.Pages;
using Shop.Toggles;
using UnityEngine;
using UnityEngine.UI;
using ToggleGroup = Shop.Toggles.ToggleGroup;

public class ShopPageView : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private PageTape _pageTape;

    public void Initialize(ShopPage shopPage, ToggleGroup shopToggleGroup)
    {
        _pageTape.Initialize(shopPage.Name, shopPage.NameTextColor, shopPage.TapeColor);
        
        foreach (var unit in shopPage.Units)
        {
            var cell = Instantiate(shopPage.CellTemplate, _grid.transform);
            cell.Initialize(unit, shopToggleGroup);
            shopToggleGroup.AddToggle(cell.Toggle);
        }
        
        //TODO: Select toggle with active skin
    }
}
