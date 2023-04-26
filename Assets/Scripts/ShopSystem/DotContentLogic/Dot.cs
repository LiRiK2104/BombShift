using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem.DotContentLogic
{
    [RequireComponent(typeof(Image))]
    public class Dot : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }
    }
}
