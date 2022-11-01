using ShopSystem.Items;
using UI;
using UnityEngine;

namespace ShopSystem.Cells.States
{
    [RequireComponent(typeof(MeshUILayerSetter))]
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
            MeshUILayerSetter.SetUILayer(preview.gameObject);
        }
    }
}
