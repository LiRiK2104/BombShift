using DataBaseSystem;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    public class SkinStorage : MonoBehaviour
    {
        private const string SavedSkinKey = "SKIN";
        
        [Inject] private DataBase _dataBase;
        
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
        
    
        public void SaveSkin(Skin skin)
        {
            skin ??= _dataBase.Core.DefaultSkin;
            PlayerPrefs.SetInt(SavedSkinKey, skin.Id);
            _savedSkin = skin;
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
    }
}
