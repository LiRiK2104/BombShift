using ShopSystem.Items;
using UnityEngine;

namespace ShopSystem.Cells.States
{
    public class LockedFragmentState : LockedState
    {
        [SerializeField] private Transform _fragmentPreviewParent;
        [SerializeField] private CustomSlider _slider;

        public CustomSlider Slider => _slider;
        
        public void SetPreview(Fragment fragment)
        {
            if (_fragmentPreviewParent == null)
                return;
            
            var preview = Instantiate(fragment, _fragmentPreviewParent);
            SetUILayer(preview.gameObject);
        }
    }
}
