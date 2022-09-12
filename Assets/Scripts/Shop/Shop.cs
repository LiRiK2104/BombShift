using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopPageView _template;
    [SerializeField] private Transform _content;
    [SerializeField] private ShopScroll _shopScroll;
    [SerializeField] private List<ShopPagePreset> _pagePresets;

    private void Awake()
    {
        List<ShopPageView> spawnedPages = new List<ShopPageView>();

        foreach (var pagePreset in _pagePresets)
        {
            var spawnedPage = Instantiate(_template, _content);

            spawnedPage.Initialize(pagePreset);
            spawnedPages.Add(spawnedPage);
        }

        _shopScroll.Initialize(spawnedPages);
    }
}
