using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShopSystem
{
    [RequireComponent(typeof(Button))]
    public class ShopButton : MonoBehaviour
    {
        [Inject] private Shop _shop;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(_shop.ShopView.Open);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_shop.ShopView.Open);
        }
    }
}
