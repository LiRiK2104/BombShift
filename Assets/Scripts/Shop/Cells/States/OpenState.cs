using UnityEngine;
using UnityEngine.UI;

namespace Shop.Cells.States
{
    public class OpenState : ShopCellState
    {
        [SerializeField] private Image _icon;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}
