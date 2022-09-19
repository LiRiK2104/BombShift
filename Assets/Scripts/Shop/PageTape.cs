using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class PageTape : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private Image _image;

        public void Initialize(string text, Color textColor, Color tapeColor)
        {
            _textMeshPro.text = text;
            _textMeshPro.color = textColor;
            _image.color = tapeColor;
        }
    }
}
