using Shop.Toggles;
using UnityEngine;

namespace Shop.Cells.States
{
    public abstract class ShopCellState : MonoBehaviour
    {
        protected const string LayerUIName = "UI";
        
        [SerializeField] private Toggle _toggle;
        [SerializeField] private GameObject _content;
        [SerializeField] private CellSelectionFrame selectionSelectionFrame;

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
            selectionSelectionFrame.gameObject.SetActive(true);
        }
    
        public void Deselect()
        {
            selectionSelectionFrame.gameObject.SetActive(false);
        }
        
        protected void SetUILayer(GameObject preview)
        {
            int targetLayer = LayerMask.NameToLayer(LayerUIName);
            
            preview.gameObject.layer = targetLayer;
            MeshRenderer[] children = preview.GetComponentsInChildren<MeshRenderer>();
            
            foreach (var child in children)
                child.gameObject.layer = targetLayer;
        }

        private void Show()
        {
            _content.SetActive(true);
        }

        private void Hide()
        {
            _content.SetActive(false);
        }
    }   
}
