using System;
using TMPro;
using UI.GrayscaleLogic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShopSystem
{
    [RequireComponent(
        typeof(Button), 
        typeof(GrayscaleGroup))]
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceTMP;
        
        [Inject] private Inventory _inventory;

        private bool _isInitialized;
        private int _price;
        private Button _button;
        private GrayscaleGroup _grayscaleGroup;


        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            _inventory.CurrencyAdded += UpdateAccess;
            UpdateAccess();
        }

        private void OnDisable()
        {
            _inventory.CurrencyAdded -= UpdateAccess;
        }

        public void UpdateInfo(int price, Action buyFunction)
        {
            if (_button == null || _grayscaleGroup == null)
                Initialize();
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => buyFunction());

            _price = price;
            _priceTMP.text = _price.ToString();
            UpdateAccess();
        }

        private void Initialize()
        {
            if (_isInitialized)
                return;

            _button = GetComponent<Button>();
            _grayscaleGroup = GetComponent<GrayscaleGroup>();
            
            _isInitialized = true;
        }

        private void UpdateAccess()
        {
            int gemsCount = _inventory.GetGemsCount();
            
            _button.interactable = gemsCount >= _price;
            _grayscaleGroup.IsGrayAll = gemsCount < _price;
        }
    }
}
