using PlayerLogic;
using ShopSystem;
using ShopSystem.Items;
using Zenject;

namespace ItemSetters
{
    public class PlayerSkinSetter : SkinSetter
    {
        [Inject] private Shop _shop;
        [Inject] private SkinStorage _skinStorage;


        private void Awake()
        {
            if (Inventory.IsLoaded)
                SetSavedSkin();
            else
                Inventory.Loaded += SetNewOrSavedSkin;
        }

        private void OnEnable()
        {
            _shop.Selected += SetSkin;
        }

        private void OnDisable()
        {
            _shop.Selected -= SetSkin;
            Inventory.Loaded -= SetNewOrSavedSkin;
        }

        public override void SetSkin(Skin skin)
        {
            if (SkinIsAvailable(skin))
            {
                base.SetSkin(skin);
                _skinStorage.SaveSkin(skin);
            }
        }

        private void SetNewOrSavedSkin()
        {
            if (_shop.TryBuyCollectedSkins(out Skin collectedSkin))
                SetSkin(collectedSkin);
            else
                SetSavedSkin();
        }

        private void SetSavedSkin()
        {
            Inventory.Loaded -= SetSavedSkin;
            SetSkin(_skinStorage.SavedSkin);
        }
    }
}
