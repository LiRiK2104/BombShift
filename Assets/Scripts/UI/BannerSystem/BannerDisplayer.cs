using System;
using System.Collections;
using System.Collections.Generic;
using RoundLogic;
using ShopSystem;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace UI.BannerSystem
{
    public class BannerDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject _blockPanel;
        [SerializeField] private SkinBanner _banner;

        [Inject] private Shop _shop;
        [Inject] private RoundRunner _roundRunner;
        
        private Queue<Skin> _skinsForShow = new Queue<Skin>();
        private bool _isDisplaying;


        private void OnEnable()
        {
            _shop.Collected += AddSkinForShow;
        }

        private void OnDisable()
        {
            _shop.Collected -= AddSkinForShow;
        }

        private void AddSkinForShow(Skin skin)
        {
            _skinsForShow.Enqueue(skin);
            Display();
        }
        
        private void Display()
        {
            if (_isDisplaying || _roundRunner.Starter.IsStarted)
                return;
            
            StartCoroutine(ProcessShowBanners());
        }
        
        private IEnumerator ProcessShowBanners()
        {
            _isDisplaying = true;
            _blockPanel.SetActive(true);

            while (_skinsForShow.Count > 0)
            {
                var skin = _skinsForShow.Dequeue();
                
                _banner.Initialize(skin);
                yield return _banner.ProcessDisplaying();
            }

            _blockPanel.SetActive(false);
            _isDisplaying = false;
        }
    }
}
