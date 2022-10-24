using System.Collections.Generic;
using Helpers;
using PlayerLogic;
using ShopSystem.Cells;
using ShopSystem.InfoBlocks;
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
        
        [SerializeField] private PageView _pageTemplate;
        [SerializeField] private Transform _content;
        [SerializeField] private ShopScroll _shopScroll;
        [SerializeField] private InfoBlocksContainer _infoBlocksContainer;

        [Inject] private Inventory _inventory;
        [Inject] private Player _player;
        
        private ToggleGroup _toggleGroup;
        private List<Page> _pages;
        private Animator _animator;
        private bool _isOpened;
        private bool _isInitialized;

        public InfoBlocksContainer InfoBlocksContainer => _infoBlocksContainer;
        
        private Animator Animator
        {
            get
            {
                if (_animator == null)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }
        

        private void OnEnable()
        {
            _shopScroll.IndexChanged += SetInfoBlock;
        }

        private void OnDisable()
        {
            _shopScroll.IndexChanged -= SetInfoBlock;
        }

        public void Initialize(List<Page> pages)
        {
            if (_isInitialized)
                return;
            
            _toggleGroup = GetComponent<ToggleGroup>();

            _pages = pages;
            List<PageView> spawnedPages = new List<PageView>();

            foreach (var page in _pages)
            {
                var spawnedPage = DiContainerRef.Container.InstantiatePrefabForComponent<PageView>(_pageTemplate, _content);

                spawnedPage.Initialize(page, _toggleGroup);
                spawnedPages.Add(spawnedPage);
            }

            var currentSkin = _player.SkinSetter.SkinPrefab;
            
            foreach (var page in spawnedPages)
            {
                if (page.TryGetCell(currentSkin, out Cell cell) && 
                    _inventory.HasSkin(currentSkin))
                {
                    _toggleGroup.SelectToggle(cell.Toggle);
                }
            }

            _shopScroll.Initialize(spawnedPages);

            _isInitialized = true;
        }

        public void Open()
        {
            if (_isOpened)
                return;
         
            _isOpened = true;
            SetAnimatorState();
            OpenLastSkinPage();
        }
        
        public void Close()
        {
            if (_isOpened == false)
                return;
            
            _isOpened = false;
            SetAnimatorState();
        }

        private Page GetPage(int index)
        {
            index = Mathf.Clamp(index, 0, _pages.Count - 1);
            return _pages[index];
        }

        private void SetInfoBlock(int pageIndex)
        {
            _infoBlocksContainer.SetInfoBlock(GetPage(pageIndex));
        }

        private void OpenLastSkinPage()
        {
            _shopScroll.Index = GetPageIndex(_player.SkinSetter.SkinPrefab);
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
        public const string OpenedStatus = "Opened";
    }
}
