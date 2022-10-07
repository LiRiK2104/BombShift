using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using PlayerLogic;
using ShopSystem.Cells;
using ShopSystem.Items;
using ShopSystem.Pages;
using ShopSystem.Toggles;
using UnityEngine;
using Zenject;

namespace ShopSystem
{
    [RequireComponent(
        typeof(Animator), 
        typeof(ToggleGroup))]
    public class ShopView : MonoBehaviour
    {
        private static readonly int OpenedState = Animator.StringToHash(ShopAnimator.OpenedStatus);
        
        [SerializeField] private ShopPageView _pageTemplate;
        [SerializeField] private Transform _content;
        [SerializeField] private ShopScroll _shopScroll;

        [Inject] private Inventory _inventory;
        [Inject] private SkinSetter _skinSetter;
        
        private ToggleGroup _toggleGroup;
        private List<ShopPage> _pages;
        private Animator _animator;
        private bool _isOpened;
        private bool _isInitialized;

        private event Action Initialized;

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
            _toggleGroup = GetComponent<ToggleGroup>();
        }

        private void OnEnable()
        {
            if (_isInitialized)
                OpenLastSkinPage();
            else
                Initialized += OpenLastSkinPage;
        }

        private void OnDisable()
        {
            Initialized -= OpenLastSkinPage;
        }

        public void Initialize(List<ShopPage> pages)
        {
            if (_isInitialized)
                return;

            _pages = pages;
            List<ShopPageView> spawnedPages = new List<ShopPageView>();

            foreach (var page in _pages)
            {
                var spawnedPage = DiContainerRef.Container.InstantiatePrefabForComponent<ShopPageView>(_pageTemplate, _content);

                spawnedPage.Initialize(page, _toggleGroup);
                spawnedPages.Add(spawnedPage);
            }

            var currentSkin = _skinSetter.Skin;
            
            foreach (var page in spawnedPages)
            {
                if (page.TryGetCell(currentSkin, out ShopCell cell) && 
                    _inventory.HasSkin(currentSkin))
                {
                    _toggleGroup.SelectToggle(cell.Toggle);
                }
            }

            _shopScroll.Initialize(spawnedPages);


            _isInitialized = true;
            Initialized?.Invoke();
        }

        public void Open()
        {
            if (_isOpened)
                return;
         
            _isOpened = true;
            SetAnimatorState();
        }
        
        public void Close()
        {
            if (_isOpened == false)
                return;
            
            _isOpened = false;
            SetAnimatorState();
        }

        private void OpenLastSkinPage()
        {
            Initialized -= OpenLastSkinPage;
            
            _shopScroll.Index = GetPageIndex(_skinSetter.Skin);
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

    static class ShopAnimator
    {
        public const float CloseAnimationDuration = 0.4f;
        public const string OpenedStatus = "Opened";
    }
}
