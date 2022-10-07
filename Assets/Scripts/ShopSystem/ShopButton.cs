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
            _button.onClick.AddListener(_shop.ShopView.Open);
        }
    }
}
