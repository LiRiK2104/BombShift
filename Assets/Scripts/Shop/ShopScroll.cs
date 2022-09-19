using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shop
{
    public class ShopScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private DotContent.DotContent _dotContent;
        [SerializeField] private float _lerpSpeed = 10;
        [SerializeField] private int _dragMilliseconds = 100;

        private bool _isInitialized;
    
        private bool _isDragging;
        private TimeSpan _beginDragTime;
        private float _beginDragX;
        private int _index;
    
        private RectTransform _content;
        private List<RectTransform> _items = new List<RectTransform>();

        private void OnValidate()
        {
            _lerpSpeed = Mathf.Max(_lerpSpeed, 0);
            _dragMilliseconds = Mathf.Max(_dragMilliseconds, 0);
        }

        private void Awake()
        {
            _content = _scrollRect.content;
        }

        private void Update()
        {
            if (_isInitialized && _isDragging == false)
            {
                ScrollToIndex();
                _dotContent.UpdateDots(_index);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _beginDragX = eventData.position.x;
            _beginDragTime = DateTime.Now.TimeOfDay;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            _index = GetNextIndex(eventData);
        }
    
        public void Initialize(List<ShopPageView> items)
        {
            if (_isInitialized)
                throw new InvalidOperationException("Already initialized");

            items.ForEach(item => _items.Add((RectTransform)item.transform));
            _dotContent.GenerateDots(items.Count);
        
            _isInitialized = true;
        }

        private int GetNextIndex(PointerEventData eventData)
        {
            TimeSpan dragTime = DateTime.Now.TimeOfDay - _beginDragTime;
            int direction = -Math.Sign(eventData.position.x - _beginDragX);

            int nextIndex = dragTime.Milliseconds < _dragMilliseconds ? 
                Mathf.Clamp(_index + direction, 0, _items.Count - 1) : 
                GetNearestIndex();

            return nextIndex;
        }

        private int GetNearestIndex()
        {
            int nearestIndex = 0;
            float nearestDistance = float.MaxValue;
            float center = _scrollRect.transform.position.x;

            for (int i = 0; i < _items.Count; i++)
            {
                float itemDistance = Mathf.Abs(center - _items[i].position.x);

                if (itemDistance < nearestDistance)
                {
                    nearestDistance = itemDistance;
                    nearestIndex = i;
                }
            }

            return nearestIndex;
        }

        private void ScrollToIndex()
        {
            RectTransform item = _items[_index];
            float contentTargetPositionX = -1 * Mathf.Clamp(item.anchoredPosition.x - item.sizeDelta.x / 2, 0, _content.sizeDelta.x);
            Vector2 nextContentPosition = new Vector2(contentTargetPositionX, _content.anchoredPosition.y);

            _content.anchoredPosition = Vector2.Lerp(_content.anchoredPosition, nextContentPosition, _lerpSpeed * Time.deltaTime);
        }
    }
}
