using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseSystem;
using Newtonsoft.Json;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace ShopSystem
{
    public class Inventory : MonoBehaviour, IInitializable
    {
        private const string SkinsKey = "SKINS";
        private const string CurrencyKey = "CURRENCY";

        [SerializeField] private Gem _gemReference;
        [SerializeField] private List<Skin> _skinsForStart = new List<Skin>();

        [Inject] private DataBase _dataBase;
        
        private List<CurrencyContainer> _currencyContainers = new List<CurrencyContainer>();
        private List<Skin> _skins = new List<Skin>();

        public event Action Loaded;
        public event Action CurrencyAdded;
        public event Action<Skin> SkinAdded;
        
        public bool IsLoaded { get; private set; }
        
        
        public void Initialize()
        {
            Load();
            IsLoaded = true;
            Loaded?.Invoke();

            foreach (var skin in _skinsForStart)
                Add(skin);
        }

        public void Add(Skin skin)
        {
            if (HasSkin(skin) == false)
            {
                _skins.Add(skin);
                SaveSkins();
                
                SkinAdded?.Invoke(skin);
            }
        }
    
        public void Add(Currency currency, int count)
        {
            if (HasCurrency(currency, out CurrencyContainer container))
            {
                container.Add(count);
            }
            else
            {
                container = new CurrencyContainer(currency, count);
                _currencyContainers.Add(container);
            }
            
            SaveCurrency();
            CurrencyAdded?.Invoke();
        }

        public void AddGems(int count)
        {
            Add(_gemReference, count);
        }

        public void Remove(Currency currency, int count)
        {
            if (HasCurrency(currency, out CurrencyContainer container))
            {
                container.Remove(count);

                if (container.Count == 0)
                    _currencyContainers.Remove(container);
                
                SaveCurrency();
            }
        }

        public void RemoveGems(int count)
        {
            Remove(_gemReference, count);
        }

        public bool HasSkin(Skin skin)
        {
            return skin == _dataBase.Core.DefaultSkin || _skins.Contains(skin);
        }

        public int GetGemsCount()
        {
            TryGetCurrencyCount(_gemReference, out int count);
            return count;
        }

        public bool TryGetCurrencyCount(Currency currency, out int count)
        {
            count = 0;
        
            if (HasCurrency(currency, out CurrencyContainer currencyContainer))
            {
                count = currencyContainer.Count;
                return true;
            }

            return false;
        }

        public void DeleteAll()
        {
            _skins.Clear();
            _currencyContainers.Clear();
            SaveSkins();
            SaveCurrency();
        }

        private bool HasCurrency(Currency currency, out CurrencyContainer foundContainer)
        {
            foundContainer = _currencyContainers.FirstOrDefault(container => container.Currency == currency);
            return foundContainer != null;
        }

        private void Load()
        {
            LoadSkins();
            LoadCurrency();
        }

        private void SaveSkins()
        {
            string json = JsonConvert.SerializeObject(_skins.Select(skin => skin.Id));
            PlayerPrefs.SetString(SkinsKey, json);
        }
    
        private void SaveCurrency()
        {
            var convertedContainers = 
                _currencyContainers.ToDictionary(container => container.Currency.Id, container => container.Count);
            
            string json = JsonConvert.SerializeObject(convertedContainers);
            PlayerPrefs.SetString(CurrencyKey, json);
        }
    
        private void LoadSkins()
        {
            string json = PlayerPrefs.GetString(SkinsKey, "");
            var skinsId = JsonConvert.DeserializeObject<int[]>(json);
        
            _skins.Clear();

            foreach (var id in skinsId)
                if (_dataBase.Core.TryGetItem(id, out Item item) &&
                    item is Skin skin &&
                    _skins.Contains(skin) == false)
                {
                    _skins.Add(skin);
                }

            var defaultSkin = _dataBase.Core.DefaultSkin;
            
            if (_skins.Contains(defaultSkin) == false)
                _skins.Add(defaultSkin);
        }

        private void LoadCurrency()
        {
            string json = PlayerPrefs.GetString(CurrencyKey, "");
            var containers = JsonConvert.DeserializeObject<Dictionary<int, int>>(json);

            if (containers == null || containers.Count == 0)
                return;
            
            _currencyContainers.Clear();
            
            foreach (var keyValuePair in containers)
                if (_dataBase.Core.TryGetItem(keyValuePair.Key, out Item item) && item is Currency currency)
                    Add(currency, keyValuePair.Value);
        }
    }
}
