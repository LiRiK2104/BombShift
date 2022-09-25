using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Shop.Items;
using Shop.Items.Controlling;
using UnityEngine;

namespace Shop
{
    public class Inventory : Singleton<Inventory>
    {
        private const string SkinsKey = "SKINS";
        private const string CurrencyKey = "CURRENCY";

        [SerializeField] private List<Skin> _skinsForStart = new List<Skin>();
        
        private List<CurrencyContainer> _currencyContainers = new List<CurrencyContainer>();
        private List<Skin> _skins = new List<Skin>();

        public event Action Loaded;
        public event Action CurrencyAdded;
        public event Action SkinAdded;
        
        public bool IsLoaded { get; private set; }


        protected override void Awake()
        {
            base.Awake();

            Load();
            IsLoaded = true;
            Loaded?.Invoke();

            foreach (var skin in _skinsForStart)
            {
                Add(skin);
            }
        }

        public void Add(Skin skin)
        {
            if (HasSkin(skin) == false)
            {
                _skins.Add(skin);
                SkinAdded?.Invoke();
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
            
            CurrencyAdded?.Invoke();
        }

        public void Remove(Currency currency, int count)
        {
            if (HasCurrency(currency, out CurrencyContainer container))
            {
                container.Remove(count);

                if (container.Count == 0)
                    _currencyContainers.Remove(container);
            }
        }

        public bool HasSkin(Skin skin)
        {
            return _skins.Contains(skin);
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

        private bool HasCurrency(Currency currency, out CurrencyContainer foundContainer)
        {
            foundContainer = _currencyContainers.FirstOrDefault(container => container.Currency == currency);
            return foundContainer != null;
        }

        private void Save()
        {
            SaveSkins();
            SaveCurrency();
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
            string json = JsonConvert.SerializeObject(_currencyContainers);
            PlayerPrefs.SetString(CurrencyKey, json);
        }
    
        private void LoadSkins()
        {
            string json = PlayerPrefs.GetString(SkinsKey, "");
            var skinsId = JsonConvert.DeserializeObject<int[]>(json);
        
            _skins.Clear();

            foreach (var id in skinsId)
                if (ItemsDataBase.Instance.Core.TryGetItem(id, out Item item) &&
                    item is Skin skin &&
                    _skins.Contains(skin) == false)
                {
                    _skins.Add(skin);
                }
        }

        private void LoadCurrency()
        {
            string json = PlayerPrefs.GetString(CurrencyKey, "");
            _currencyContainers = JsonConvert.DeserializeObject<List<CurrencyContainer>>(json) ?? _currencyContainers;
        }
    }
}
