using UnityEngine;
using UnityEngine.UI;

namespace Shop.Cells.States
{
    public class LockedFragmentState : LockedState
    {
        [SerializeField] private Image _fragmentIcon;
        [SerializeField] private CustomSlider _slider;

        public CustomSlider Slider => _slider;
        
        public void SetFragmentIcon(Sprite icon)
        {
            _fragmentIcon.sprite = icon;
        }
    }
}
