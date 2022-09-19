using Shop.Cells.States;
using Shop.Toggles;
using Shop.Units;
using UnityEngine;

namespace Shop.Cells
{
    public abstract class ShopCell : MonoBehaviour
    {
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private OpenState _openState;

        protected abstract LockedState LockedState { get; }

        private bool _initialized;

        public virtual void Initialize(ShopUnit shopUnit)
        {
            if (_initialized)
                return;

            _openState.SetIcon(shopUnit.Skin.Icon);
            _toggleGroup.AddToggle(_openState.Toggle);
            _toggleGroup.AddToggle(LockedState.Toggle);
        
            if (ItemStorage.Instance.HasSkin(shopUnit.Skin))
                Open();
            else
                Close();


            _initialized = true;
        }

        public void Open()
        {
            _toggleGroup.SelectToggle(_openState.Toggle);
        }
    
        public void Close()
        {
            _toggleGroup.SelectToggle(LockedState.Toggle);
        }
    }
}
