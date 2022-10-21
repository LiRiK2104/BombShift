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
        
        private int _price;
        private Button _button;
        private GrayscaleGroup _grayscaleGroup;
        

        private void OnEnable()
        {
            _inventory.CurrencyAdded += UpdateAccess;
            UpdateAccess();
        }

        private void OnDisable()
        {
            _inventory.CurrencyAdded -= UpdateAccess;
        }

        public void Initialize(int price)
        {
            _button = GetComponent<Button>();
            _grayscaleGroup = GetComponent<GrayscaleGroup>();
            _grayscaleGroup.Initialize();
            
            _price = price;
            _priceTMP.text = _price.ToString();
            UpdateAccess();
        }

        private void UpdateAccess()
        {
            int gemsCount = _inventory.GetGemsCount();
            
            _button.interactable = gemsCount >= _price;
            _grayscaleGroup.IsGrayAll = gemsCount < _price;
        }
    }
}
