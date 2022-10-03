using Player;
using ShopSystem.Cells.States;
using ShopSystem.Units;
using UnityEngine;
using UnityEngine.UI;
using ToggleGroup = ShopSystem.Toggles.ToggleGroup;
using Toggle = ShopSystem.Toggles.Toggle;

namespace ShopSystem.Cells
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

        public Toggle Toggle => _toggle;
        public ShopUnit ShopUnit { get; private set; }
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

        protected virtual void OnEnable()
        {
            Inventory.Instance.SkinAdded += UpdateState;
            
            _toggle.Activating += ActiveState.Select;
            _toggle.Deactivating += ActiveState.Deselect;
            _button.onClick.AddListener(Select);
        }

        protected virtual void OnDisable()
        {
            Inventory.Instance.SkinAdded -= UpdateState;
            
            _toggle.Activating -= ActiveState.Select;
            _toggle.Deactivating -= ActiveState.Deselect;
            _button.onClick.RemoveListener(Select);
        }

        public virtual void Initialize(ShopUnit shopUnit, ToggleGroup shopPageToggleGroup)
        {
            if (_initialized)
                return;

            _shopToggleGroup = shopPageToggleGroup;
            ShopUnit = shopUnit;

            _openState.SetPreview(shopUnit.Skin);
            _statesToggleGroup.AddToggle(_openState.Toggle);
            _statesToggleGroup.AddToggle(LockedState.Toggle);
            UpdateState();

            _initialized = true;
        }

        private void UpdateState()
        {
            if (Inventory.Instance.HasSkin(ShopUnit.Skin))
                Open();
            else
                Close();
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

            if (Inventory.Instance.HasSkin(ShopUnit.Skin))
            {
                SkinSetter.Instance.SetSkin(ShopUnit.Skin);
                _shopToggleGroup.SelectToggle(_toggle);
            }
        }
    }
}
