using System;
using ShopSystem.Items;
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
        [Inject] private Inventory _inventory;

        //TODO: инициализировать из InfoBlock
        private int _price;
        private Button _button;
        private GrayscaleGroup _grayscaleGroup;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _grayscaleGroup = GetComponent<GrayscaleGroup>();
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

        private void UpdateAccess()
        {
            int gemsCount = _inventory.GetGemsCount();
            
            _button.interactable = gemsCount >= _price;
            _grayscaleGroup.IsGrayAll = gemsCount < _price;
        }
    }
}
