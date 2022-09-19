using Shop;
using Shop.Pages;
using UnityEngine;
using UnityEngine.UI;

public class ShopPageView : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private PageTape _pageTape;
    
    public void Initialize(ShopPage shopPage)
    {
        _pageTape.Initialize(shopPage.Name, shopPage.NameTextColor, shopPage.TapeColor);
        
        foreach (var unit in shopPage.Units)
        {
            var cell = Instantiate(shopPage.CellTemplate, _grid.transform);
            cell.Initialize(unit);
        }
    }
}
