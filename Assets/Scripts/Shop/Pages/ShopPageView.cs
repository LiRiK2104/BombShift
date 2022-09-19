using Shop.Pages;
using UnityEngine;

public class ShopPageView : MonoBehaviour
{
    public void Initialize(ShopPage shopPage)
    {
        foreach (var unit in shopPage.Units)
        {
            var cell = Instantiate(shopPage.CellTemplate, transform);
            cell.Initialize(unit);
        }
    }
}
