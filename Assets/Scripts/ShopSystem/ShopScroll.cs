using System;
using System.Collections.Generic;
using ShopSystem.Pages;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShopSystem
{
    public class ShopScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        private const int NullIndex = -1;
        
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private DotContentLogic.DotContent _dotContent;
        [SerializeField] private float _lerpSpeed = 10;
        [SerializeField] private int _dragMilliseconds = 100;

        private bool _isInitialized;
        private bool _isDragging;
        private TimeSpan _beginDragTime;
        private float _beginDragX;
        private int _index = NullIndex;
        private List<RectTransform> _items = new List<RectTransform>();

        public event Action<int> IndexChanged;

        public int Index
        {
            get
            {
                return _index == NullIndex ? 0 : _index;
            }
            
            set
            {
                int lastIndex = _index;
                _index = Mathf.Clamp(value, 0, _items.Count);

                if (_index != lastIndex)
                    IndexChanged?.Invoke(_index);
            }
        }
        
        private RectTransform Content => _scrollRect.content;

        
        private void OnValidate()
        {
            _lerpSpeed = Mathf.Max(_lerpSpeed, 0);
            _dragMilliseconds = Mathf.Max(_dragMilliseconds, 0);
        }

        private void Update()
        {
            if (_isInitialized && _isDragging == false)
            {
                ScrollToIndex();
                _dotContent.UpdateDots(Index);
            }
        }
        
        public void Initialize(List<PageView> items)
        {
            if (_isInitialized)
                throw new InvalidOperationException("Already initialized");

            items.ForEach(item => _items.Add((RectTransform)item.transform));
            _dotContent.GenerateDots(items.Count);
        
            _isInitialized = true;
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
            Index = GetNextIndex(eventData);
        }
        
        public void ScrollToIndexInstantly()
        {
            Content.anchoredPosition = GetNextContentPosition();
        }

        private int GetNextIndex(PointerEventData eventData)
        {
            TimeSpan dragTime = DateTime.Now.TimeOfDay - _beginDragTime;
            int direction = -Math.Sign(eventData.position.x - _beginDragX);

            int nextIndex = dragTime.Milliseconds < _dragMilliseconds ? 
                Mathf.Clamp(Index + direction, 0, _items.Count - 1) : 
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
            Content.anchoredPosition = Vector2.Lerp(Content.anchoredPosition, GetNextContentPosition(), _lerpSpeed * Time.deltaTime);
        }

        private Vector2 GetNextContentPosition()
        {
            RectTransform page = _items[Index];
            float leftContentBoundX = 0;
            float rightContentBoundX = Mathf.Abs(Content.sizeDelta.x);
            float pagePosition = page.anchoredPosition.x - page.sizeDelta.x / 2;

            float contentTargetPositionX = -1 * Mathf.Clamp(pagePosition, leftContentBoundX, rightContentBoundX);
            return new Vector2(contentTargetPositionX, Content.anchoredPosition.y);
        }
    }
}
