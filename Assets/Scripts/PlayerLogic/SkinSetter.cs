using DataBaseSystem;
using ShopSystem;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    public class SkinSetter : MonoBehaviour, IInitializable
    {
        private const string _savedSkinKey = "SKIN";

        [SerializeField] private Skin _defaultSkin;

        [Inject] private DataBase _dataBase;
        [Inject] private Inventory _inventory;

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
        

        private void OnDisable()
        {
            _inventory.Loaded -= SetSavedSkin;
        }
        
        public void Initialize()
        {
            if (_inventory.IsLoaded)
                SetSavedSkin();
            else
                _inventory.Loaded += SetSavedSkin;
        }

        public void SetSkin(Skin skin)
        {
            if (_inventory.HasSkin(skin))
            {
                _skin = skin;
                SaveSkin();
                //TODO: устанавливать скин на игрока
            }
        }
    
        private void SetSavedSkin()
        {
            _inventory.Loaded -= SetSavedSkin;
        
            if (_inventory.HasSkin(_skin))
            {
                //TODO: устанавливать скин на игрока
            }
        }

        private void LoadSkin()
        {
            int id = PlayerPrefs.GetInt(_savedSkinKey, _defaultSkin.Id);
            _skin = _defaultSkin;

            if (_dataBase.Core.TryGetItem(id, out Item item) && 
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
