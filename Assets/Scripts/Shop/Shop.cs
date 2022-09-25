using System;
using System.Collections.Generic;
using Shop.Items;
using Shop.Pages;
using Shop.Toggles;
using UnityEngine;

namespace Shop
{
    [RequireComponent(typeof(Animator))]
    public class Shop : MonoBehaviour
    {
        private static readonly int OpenedState = Animator.StringToHash(ShopAnimator.OpenedStatus);
        
        [SerializeField] private ShopPageView _pageTemplate;
        [SerializeField] private Transform _content;
        [SerializeField] private ShopScroll _shopScroll;
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private List<ShopPage> _pages;

        private Animator _animator;
        private bool _isOpened;

        private Animator Animator
        {
            get
            {
                if (_animator == null)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }

        private void Awake()
        {
            List<ShopPageView> spawnedPages = new List<ShopPageView>();

            foreach (var pagePreset in _pages)
            {
                var spawnedPage = Instantiate(_pageTemplate, _content);

                spawnedPage.Initialize(pagePreset, _toggleGroup);
                spawnedPages.Add(spawnedPage);
            }

            _shopScroll.Initialize(spawnedPages);
        }

        private void OnEnable()
        {
            
        }

        public void Open()
        {
            if (_isOpened)
                return;
         
            _isOpened = true;
            gameObject.SetActive(true);
            SetAnimatorState();
        }
        
        public void Close()
        {
            if (_isOpened == false)
                return;
            
            _isOpened = false;
            gameObject.SetActive(false);
            SetAnimatorState();
        }

        private void OpenLastSkinPage()
        {
            //TODO: получить из класса SkinSetter надетый скин. Вызывать в OnEnable().
            Skin skin = new Skin();
            _shopScroll.Index = GetPageIndex(skin);
            _shopScroll.ScrollToIndexInstantly();
        }
        
        private int GetPageIndex(Skin targetSkin)
        {
            int index = 0;
            
            for (int i = 0; i < _pages.Count; i++)
            {
                foreach (var unit in _pages[i].Units)
                {
                    if (unit.Skin == targetSkin)
                        index = i;
                }
            }

            return index;
        }

        private void SetAnimatorState()
        {
            Animator.SetBool(OpenedState, _isOpened);
        }
    }

    class ShopAnimator
    {
        public const string OpenedStatus = "Opened";
    }
}
