using System;
using System.Collections.Generic;
using DataBaseSystem;
using PlayerLogic.Spirit;
using ShopSystem;
using ShopSystem.Cells;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    public class SkinSetter : MonoBehaviour
    {
        private const string _savedSkinKey = "SKIN";

        [Inject] private DataBase _dataBase;
        [Inject] private Inventory _inventory;
        [Inject] private Shop _shop;

        private Skin _skinPrefab;
        private List<Skin> _createdSkins = new List<Skin>();

        public event Action<Skin> SkinSetted;

        public Skin SkinPrefab
        {
            get
            {
                if (_skinPrefab == null)
                    LoadSkin();

                return _skinPrefab;
            }
        }


        private void Awake()
        {
            if (_inventory.IsLoaded)
                SetSavedSkin();
            else
                _inventory.Loaded += SetSavedSkin;
        }

        private void OnEnable()
        {
            _shop.Selected += SetSkin;
        }

        private void OnDisable()
        {
            _shop.Selected -= SetSkin;
            _inventory.Loaded -= SetSavedSkin;
        }

        private void SetSkin(Skin skin)
        {
            if (_inventory.HasSkin(skin))
            {
                _skinPrefab = skin;
                SaveSkin();
                DisableAllCreatedSkins();

                if (TryGetCreatedSkin(_skinPrefab, out Skin foundSkin))
                    foundSkin.gameObject.SetActive(true);
                else
                    _createdSkins.Add(CreateSkin(_skinPrefab));
                
                SkinSetted?.Invoke(skin);
            }
        }

        private void SetSavedSkin()
        {
            _inventory.Loaded -= SetSavedSkin;
            SetSkin(SkinPrefab);
        }

        private bool TryGetCreatedSkin(Skin needSkin, out Skin foundSkin)
        {
            foundSkin = null;

            foreach (var createdSkin in _createdSkins)
            {
                if (createdSkin.Id == needSkin.Id)
                {
                    foundSkin = createdSkin;
                    return true;
                }
            }

            return false;
        }

        private Skin CreateSkin(Skin skinPrefab)
        {
            return Instantiate(skinPrefab, transform);
        }
        
        private void DisableAllCreatedSkins()
        {
            foreach (var createdSkin in _createdSkins)
                createdSkin.gameObject.SetActive(false);
        }
        
        private void LoadSkin()
        {
            var defaultSkin = _dataBase.Core.DefaultSkin;
            int id = PlayerPrefs.GetInt(_savedSkinKey, defaultSkin.Id);
            _skinPrefab = defaultSkin;

            if (_dataBase.Core.TryGetItem(id, out Item item) && 
                item is Skin skin)
                _skinPrefab = skin;
        }
    
        private void SaveSkin()
        {
            if (_skinPrefab == null)
                _skinPrefab = _dataBase.Core.DefaultSkin;
        
            PlayerPrefs.SetInt(_savedSkinKey, _skinPrefab.Id);   
        }
    }
}
