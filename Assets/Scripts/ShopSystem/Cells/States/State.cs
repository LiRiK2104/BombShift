using ShopSystem.Toggles;
using UI;
using UnityEngine;

namespace ShopSystem.Cells.States
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private GameObject _content;
        [SerializeField] private SelectionFrame selectionSelectionFrame;

        private MeshUILayerSetter _meshUILayerSetter;
        
        public Toggle Toggle => _toggle;

        protected MeshUILayerSetter MeshUILayerSetter
        {
            get
            {
                if (_meshUILayerSetter == null)
                    _meshUILayerSetter = GetComponent<MeshUILayerSetter>();

                return _meshUILayerSetter;
            }
        }
        
        
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

        private void Show()
        {
            _content.SetActive(true);
        }

        private void Hide()
        {
            _content.SetActive(false);
            Deselect();
        }
    }   
}
