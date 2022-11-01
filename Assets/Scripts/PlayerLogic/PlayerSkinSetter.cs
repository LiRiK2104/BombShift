using DataBaseSystem;
using ShopSystem;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    public class PlayerSkinSetter : SkinSetter
    {
        private const string SavedSkinKey = "SKIN";

        [Inject] private DataBase _dataBase;
        [Inject] private Shop _shop;

        private Skin _savedSkin;

        public Skin SavedSkin
        {
            get
            {
                if (_savedSkin == null)
                    LoadSkin();

                return _savedSkin;
            }
        }


        private void Awake()
        {
            if (Inventory.IsLoaded)
                SetSavedSkin();
            else
                Inventory.Loaded += SetSavedSkin;
        }

        private void OnEnable()
        {
            _shop.Selected += SetSkin;
        }

        private void OnDisable()
        {
            _shop.Selected -= SetSkin;
            Inventory.Loaded -= SetSavedSkin;
        }

        public override void SetSkin(Skin skin)
        {
            if (SkinIsAvailable(skin))
            {
                base.SetSkin(skin);
                SaveSkin();
                _savedSkin = skin;
            }
        }

        private void SetSavedSkin()
        {
            Inventory.Loaded -= SetSavedSkin;
            SetSkin(SavedSkin);
        }

        private void LoadSkin()
        {
            var defaultSkin = _dataBase.Core.DefaultSkin;
            int id = PlayerPrefs.GetInt(SavedSkinKey, defaultSkin.Id);
            _savedSkin = defaultSkin;

            if (_dataBase.Core.TryGetItem(id, out Item item) && 
                item is Skin skin)
                _savedSkin = skin;
        }
    
        private void SaveSkin()
        {
            if (_savedSkin == null)
                _savedSkin = _dataBase.Core.DefaultSkin;
        
            PlayerPrefs.SetInt(SavedSkinKey, _savedSkin.Id);   
        }
    }
}
