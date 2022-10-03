using Helpers;
using ShopSystem;
using ShopSystem.Items;
using ShopSystem.Items.Controlling;
using UnityEngine;

namespace PlayerLogic
{
    public class SkinSetter : Singleton<SkinSetter>
    {
        private const string _savedSkinKey = "SKIN";

        [SerializeField] private Skin _defaultSkin;

        private Skin _skin;

        public Skin Skin
        {
            get
            {
                if (_skin == null)
                    LoadSkin();

                return _skin;
            }
        }

        protected override void Awake()
        {
            base.Awake();
        
            if (Inventory.Instance.IsLoaded)
                SetSavedSkin();
            else
                Inventory.Instance.Loaded += SetSavedSkin;
        }

        private void OnDisable()
        {
            Inventory.Instance.Loaded -= SetSavedSkin;
        }

        public void SetSkin(Skin skin)
        {
            if (Inventory.Instance.HasSkin(skin))
            {
                _skin = skin;
                SaveSkin();
                //TODO: устанавливать скин на игрока
            }
        }
    
        private void SetSavedSkin()
        {
            Inventory.Instance.Loaded -= SetSavedSkin;
        
            if (Inventory.Instance.HasSkin(_skin))
            {
                //TODO: устанавливать скин на игрока
            }
        }

        private void LoadSkin()
        {
            int id = PlayerPrefs.GetInt(_savedSkinKey, _defaultSkin.Id);
            _skin = _defaultSkin;

            if (ItemsDataBase.Instance.Core.TryGetItem(id, out Item item) && 
                item is Skin skin)
                _skin = skin;
        }
    
        private void SaveSkin()
        {
            if (_skin == null)
                _skin = _defaultSkin;
        
            PlayerPrefs.SetInt(_savedSkinKey, _skin.Id);   
        }
    }
}
