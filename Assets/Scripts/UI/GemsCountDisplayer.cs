using ShopSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GemsCountDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        [Inject] private Inventory _inventory;

        private void OnEnable()
        {
            _inventory.CurrencyAdded += UpdateCountText;
            UpdateCountText();
        }

        private void OnDisable()
        {
            _inventory.CurrencyAdded -= UpdateCountText;
        }

        private void UpdateCountText()
        {
            _textMeshPro.text = _inventory.GetGemsCount().ToString();
        }
    }
}
