using DataBaseSystem;
using ShopSystem;
using ShopSystem.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dev
{
    public sealed class CurrencyAdder : MonoBehaviour
    {
        private const string DefaultResultText = "-";
        private const string CompleteResultText = "complete!";
        private const string FailResultText = "failed!";
        
        [SerializeField] private TMP_InputField _idInputField;
        [SerializeField] private TMP_InputField _countInputField;
        [SerializeField] private Button _addCurrencyButton;
        [SerializeField] private TextMeshProUGUI _resultText;

        [Inject] private DataBase _dataBase;
        [Inject] private Inventory _inventory;


        private void OnEnable()
        {
            LogResult(Result.None);
        }

        private void Awake()
        {
#if UNITY_EDITOR
            _addCurrencyButton.onClick.AddListener(AddCurrency);
            _countInputField.onEndEdit.AddListener( delegate { ClampCount(); });
#endif
        }

        private void AddCurrency()
        {
#if UNITY_EDITOR
            if (int.TryParse(_idInputField.text, out int id) &&
                _dataBase.Core.TryGetItem(id, out Item foundItem) && foundItem is Currency currency && 
                int.TryParse(_countInputField.text, out int count))
            {
                _inventory.Add(currency, count);
                LogResult(Result.Complete);
            }
            else
            {
                LogResult(Result.Fail);
            }
#endif
        }

        private void LogResult(Result result)
        {
#if UNITY_EDITOR
            string resultText;

            switch (result)
            {
                case Result.Complete:
                    resultText = CompleteResultText;
                    break;
                
                case Result.Fail:
                    resultText = FailResultText;
                    break;
                
                case Result.None:
                default:
                    resultText = DefaultResultText;
                    break;
            }

            _resultText.text = resultText;
#endif
        }

        private void ClampCount()
        {
            int minCount = 0;
            int maxCount = 10000;
            string text = _countInputField.text;

            int.TryParse(text, out int count);
            count = Mathf.Clamp(count, minCount, maxCount);
            _countInputField.text = count.ToString();
        }

        private enum Result
        {
            None,
            Fail,
            Complete
        }
    }
}
