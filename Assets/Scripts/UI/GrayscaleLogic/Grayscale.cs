using UnityEngine;
using UnityEngine.UI;

namespace UI.GrayscaleLogic
{
    [RequireComponent(typeof(Image))]
    public class Grayscale : MonoBehaviour
    {
        [SerializeField] private Material _grayscaleMaterial;
        
        private Image _image;
        private Color _idleColor;
        private Color _grayscaleColor;
        private bool _isGray;

        public bool IsGray
        {
            get => _isGray;
            set
            {
                _isGray = value;
                SwitchGrayscale(_isGray);
            }
        }
        

        private void Awake()
        {
            _image = GetComponent<Image>();
            _idleColor = _image.color;
            _grayscaleColor = new Color(_idleColor.grayscale, _idleColor.grayscale, _idleColor.grayscale, _idleColor.a);

            _image.material = _grayscaleMaterial;
        }

        private void SwitchGrayscale(bool isGrayscale)
        {
            string grayscaleParameter = "_GrayscaleAmount";
            float amount = isGrayscale ? 1 : 0;
            
            _image.color = isGrayscale ? _grayscaleColor : _idleColor;
            _image.material.SetFloat(grayscaleParameter, amount);
        }
    }
}
