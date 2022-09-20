using Shop.Items;
using UnityEngine;

namespace Shop.Cells.States
{
    public class OpenState : ShopCellState
    {
        [SerializeField] private Transform _previewParent;

        public void SetPreview(Skin skin)
        {
            if (_previewParent == null)
                return;
            
            var preview = Instantiate(skin, _previewParent);
            SetUILayer(preview.gameObject);
        }
    }
}
