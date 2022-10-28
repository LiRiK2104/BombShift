using ShopSystem.Cells.States;
using ShopSystem.InfoBlocks;
using ShopSystem.Items;
using ShopSystem.Units;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ToggleGroup = ShopSystem.Toggles.ToggleGroup;
using Toggle = ShopSystem.Toggles.Toggle;

namespace ShopSystem.Cells
{
    [RequireComponent(typeof(CellView))]
    public abstract class Cell : MonoBehaviour
    {
        [SerializeField] private ToggleGroup _statesToggleGroup;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Button _button;
        [SerializeField] private OpenState _openState;

        [Inject] protected Inventory Inventory;
        [Inject] private Shop _shop;

        private bool _initialized;
        private State _activeState;
        private ToggleGroup _shopToggleGroup;
        private CellView _cellView;
        
        public bool IsOpened { get; private set; }
        public Toggle Toggle => _toggle;
        public CellView CellView => _cellView;
        public Unit Unit { get; private set; }
        protected abstract LockedState LockedState { get; }

        private State ActiveState
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
        

        private void Awake()
        {
            _cellView = GetComponent<CellView>();
        }

        protected virtual void OnEnable()
        {
            Inventory.SkinAdded += skin => UpdateState();
            Inventory.SkinAdded += TrySelect;

            _toggle.Activating += ActiveState.Select;
            _toggle.Deactivating += ActiveState.Deselect;
            _button.onClick.AddListener(Select);
        }

        protected virtual void OnDisable()
        {
            Inventory.SkinAdded -= skin => UpdateState();
            Inventory.SkinAdded -= TrySelect;
            
            _toggle.Activating -= ActiveState.Select;
            _toggle.Deactivating -= ActiveState.Deselect;
            _button.onClick.RemoveListener(Select);
        }

        public virtual void Initialize(Unit unit, ToggleGroup shopPageToggleGroup)
        {
            if (_initialized)
                return;

            _shopToggleGroup = shopPageToggleGroup;
            Unit = unit;

            _openState.SetPreview(unit.Skin);
            _statesToggleGroup.AddToggle(_openState.Toggle);
            _statesToggleGroup.AddToggle(LockedState.Toggle);
            UpdateState();

            _initialized = true;
        }

        private void UpdateState()
        {
            if (Inventory.HasSkin(Unit.Skin))
                Open();
            else
                Close();
        }

        private void Open()
        {
            _statesToggleGroup.SelectToggle(_openState.Toggle);
            ActiveState = _openState;
            IsOpened = true;
        }
    
        private void Close()
        {
            _statesToggleGroup.SelectToggle(LockedState.Toggle);
            ActiveState = LockedState;
            IsOpened = false;
        }

        private void TrySelect(Skin skin)
        {
            if (skin == Unit.Skin)
                Select();
        }

        private void Select()
        {
            if (_initialized == false)
                return;

            if (Inventory.HasSkin(Unit.Skin))
            {
                _shopToggleGroup.SelectToggle(_toggle);
                _shop.OnSelectedCell(this);
            }
            else if (Unit is IInfoBlockOwner infoBlockOwner)
            {
                _shop.ShopView.InfoBlocksContainer.UpdateInfoBlock(infoBlockOwner);
            }
        }
    }
}
