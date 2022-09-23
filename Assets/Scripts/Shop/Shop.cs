using System.Collections.Generic;
using Shop.Pages;
using Shop.Toggles;
using UnityEngine;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopPageView _pageTemplate;
        [SerializeField] private Transform _content;
        [SerializeField] private ShopScroll _shopScroll;
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private List<ShopPage> _pages;

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
    }
}
