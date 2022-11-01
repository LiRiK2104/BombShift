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
        
        [SerializeField] private Transform _content;
        [SerializeField] private ShopScroll _shopScroll;
        [SerializeField] private InfoBlocksContainer _infoBlocksContainer;
        [SerializeField] private RouletteStarter _rouletteStarter;

        [Inject] private Inventory _inventory;
        [Inject] private Player _player;
        
        private List<PageView> _spawnedPages;
        private ToggleGroup _unitsToggleGroup;
        private List<Page> _pages;
        private Animator _animator;
        private bool _isOpened;
        private bool _isInitialized;

        public InfoBlocksContainer InfoBlocksContainer => _infoBlocksContainer;
        public RouletteStarter RouletteStarter => _rouletteStarter;
        
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
            if (_isInitialized)
                Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        public void Initialize(List<Page> pages)
        {
            if (_isInitialized)
                return;
            
            _unitsToggleGroup = GetComponent<ToggleGroup>();

            _pages = pages;
            _spawnedPages = new List<PageView>();

            foreach (var page in _pages)
            {
                var spawnedPage = DiContainerRef.Container.InstantiatePrefabForComponent<PageView>(page.PageViewTemplate, _content);

                spawnedPage.Initialize(page, _unitsToggleGroup);
                _spawnedPages.Add(spawnedPage);
            }

            var currentSkin = _player.PlayerSkinSetter.SavedSkin;
            
            foreach (var pageView in _spawnedPages)
            {
                if (pageView.TryGetCell(currentSkin, out Cell cell) && 
                    _inventory.HasSkin(currentSkin))
                {
                    _unitsToggleGroup.SelectToggle(cell.Toggle);
                }
            }

            _shopScroll.Initialize(_spawnedPages);

            _isInitialized = true;
            Subscribe();
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

        private void Subscribe()
        {
            _shopScroll.IndexChanged += UpdateInfoBlock;
            _shopScroll.IndexChanged += SetRoulettePage;
            _inventory.SkinAdded += skin => UpdateInfoBlock(); 
        }
        
        private void Unsubscribe()
        {
            _shopScroll.IndexChanged -= UpdateInfoBlock;
            _shopScroll.IndexChanged -= SetRoulettePage;
            _inventory.SkinAdded -= skin => UpdateInfoBlock();    
        }

        private Page GetPage(int index)
        {
            index = Mathf.Clamp(index, 0, _pages.Count - 1);
            return _pages[index];
        }

        private PageView GetSpawnedPage(int index)
        {
            index = Mathf.Clamp(index, 0, _spawnedPages.Count - 1);
            return _spawnedPages[index];
        }

        private void UpdateInfoBlock(int pageIndex)
        {
            _infoBlocksContainer.UpdateInfoBlock(GetPage(pageIndex));
        }
        
        private void UpdateInfoBlock()
        {
            _infoBlocksContainer.UpdateInfoBlock(GetPage(_shopScroll.Index));
        }

        private void SetRoulettePage(int pageIndex)
        {
            var pageView = GetSpawnedPage(pageIndex);
            
            if (pageView is RoulettePageView roulettePageView)
                _rouletteStarter.PageView = roulettePageView;
        }

        private void OpenLastSkinPage()
        {
            _shopScroll.Index = GetPageIndex(_player.PlayerSkinSetter.SavedSkin);
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
