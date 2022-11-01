using System.Collections;
using System.Collections.Generic;
using PlayerLogic;
using ShopSystem.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BannerSystem
{
    [RequireComponent(typeof(UISkinSetter))]
    public class SkinBanner : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private UISkinSetter _skinSetter;
        private List<Skin> _createdSkins;

        private bool _needToHide;
        private bool _isDisplaying;
        
        
        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
            _skinSetter = GetComponent<UISkinSetter>();
            Hide();
        }
        
        public void Initialize(Skin skin)
        {
            _skinSetter.SetSkin(skin);
        }
        
        public IEnumerator ProcessDisplaying()
        {
            if (_isDisplaying)
                yield break;
            
            _isDisplaying = true;
            Show();
            yield return new WaitUntil(() => _needToHide);
            Hide();
            _isDisplaying = false;
        }
        
        private void Close()
        {
            _needToHide = true;
        }
        
        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            _needToHide = false;
        }
    }
}
