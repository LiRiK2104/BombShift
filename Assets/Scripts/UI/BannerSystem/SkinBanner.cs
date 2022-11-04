using System.Collections;
using System.Collections.Generic;
using ShopSystem.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BannerSystem
{
    [RequireComponent(typeof(UISkinSetter), typeof(Animator))]
    public class SkinBanner : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private UISkinSetter _skinSetter;
        private List<Skin> _createdSkins;
        private Animator _animator;

        private bool _needToHide;
        private bool _isDisplaying;
        
        
        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
            _skinSetter = GetComponent<UISkinSetter>();
            _animator = GetComponent<Animator>();
            
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
            _animator.SetBool(SkinBannerAnimator.Flags.Showed, true);
        }

        private void Hide()
        {
            _animator.SetBool(SkinBannerAnimator.Flags.Showed, false);
        }
    }

    public static class SkinBannerAnimator
    {
        public static class Flags
        {
            public static string Showed = "Showed";
        }
    }
}
