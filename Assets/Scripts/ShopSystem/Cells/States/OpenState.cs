using Helpers;
using ShopSystem.Items;
using UnityEngine;

namespace ShopSystem.Cells.States
{
    public class OpenState : ShopCellState
    {
        [SerializeField] private Transform _previewParent;

        public void SetPreview(Skin skin)
        {
            if (_previewParent == null)
                return;
            
            var preview = DiContainerRef.Container.InstantiatePrefab(skin, _previewParent);
            SetUILayer(preview.gameObject);
        }
    }
}
