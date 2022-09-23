using System;
using Shop.Cells.States;
using Shop.Toggles;
using Shop.Units;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ToggleGroup = Shop.Toggles.ToggleGroup;
using Toggle = Shop.Toggles.Toggle;

namespace Shop.Cells
{
    public abstract class ShopCell : MonoBehaviour
    {
        [SerializeField] private ToggleGroup _statesToggleGroup;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Button _button;
        [SerializeField] private OpenState _openState;

        private bool _initialized;
        private ShopCellState _activeState;
        private ToggleGroup _shopToggleGroup;
        private ShopUnit _shopUnit;
        
        public Toggle Toggle => _toggle;
        protected abstract LockedState LockedState { get; }

        private ShopCellState ActiveState
        {
            get
            {
                if (_activeState == null)
                    _activeState = LockedState;
                
                return _activeState;
            }

            set
            {
                _activeState = value;
            }
        }

        private void OnEnable()
        {
            _toggle.Activating += ActiveState.Select;
            _toggle.Deactivating += ActiveState.Deselect;
            _button.onClick.AddListener(Select);
        }

        private void OnDisable()
        {
            _toggle.Activating -= ActiveState.Select;
            _toggle.Deactivating -= ActiveState.Deselect;
            _button.onClick.RemoveListener(Select);
        }

        public virtual void Initialize(ShopUnit shopUnit, ToggleGroup shopPageToggleGroup)
        {
            if (_initialized)
                return;

            _shopToggleGroup = shopPageToggleGroup;
            _shopUnit = shopUnit;

            _openState.SetPreview(shopUnit.Skin);
            _statesToggleGroup.AddToggle(_openState.Toggle);
            _statesToggleGroup.AddToggle(LockedState.Toggle);
        
            if (ItemStorage.Instance.HasSkin(shopUnit.Skin))
                Open();
            else
                Close();
            

            _initialized = true;
        }

        private void Open()
        {
            _statesToggleGroup.SelectToggle(_openState.Toggle);
            ActiveState = _openState;
        }
    
        private void Close()
        {
            _statesToggleGroup.SelectToggle(LockedState.Toggle);
            ActiveState = LockedState;
        }

        private void Select()
        {
            if (_initialized == false)
                return;
            
            if (ItemStorage.Instance.HasSkin(_shopUnit.Skin))
                _shopToggleGroup.SelectToggle(_toggle);
            /*else
                _shopPageToggleGroup.SelectToggleAndSaveLast(_toggle);*/
        }
    }
}
