using Shop.Toggles;
using UnityEngine;

namespace Shop.Cells.States
{
    public abstract class ShopCellState : MonoBehaviour
    {
        private const float ActiveAlpha = 1;
        private const float DisactiveAlpha = 0;
        
        [SerializeField] private Toggle _toggle;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _selectionFrame;

        public Toggle Toggle => _toggle;
        
        private void OnEnable()
        {
            _toggle.Activating += Show;
            _toggle.Deactivating += Hide;
        }

        private void OnDisable()
        {
            _toggle.Activating -= Show;
            _toggle.Deactivating -= Hide;
        }
        
        public void Select()
        {
            _selectionFrame.SetActive(true);
        }
    
        public void Deselect()
        {
            _selectionFrame.SetActive(false);
        }

        private void Show()
        {
            _canvasGroup.alpha = ActiveAlpha;
        }

        private void Hide()
        {
            _canvasGroup.alpha = DisactiveAlpha;
        }
    }   
}
