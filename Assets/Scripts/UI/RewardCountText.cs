using TMPro;
using UnityEngine;

namespace UI
{
    public class RewardCountText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [Space]
        [SerializeField] private TMP_FontAsset _idleFont;
        [SerializeField] private TMP_FontAsset _multipliedFont;

        public TextMeshProUGUI TextMeshPro => _textMeshPro;
        
        
        public void SetIdleState()
        {
            _textMeshPro.font = _idleFont;
        }

        public void SetMultipliedState()
        {
            _textMeshPro.font = _multipliedFont;
        }
    }
}
