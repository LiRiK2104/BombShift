using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using ShopSystem.Cells;
using ShopSystem.Items;
using UnityEngine;
using UnityEngine.UI;
using ToggleGroup = ShopSystem.Toggles.ToggleGroup;

namespace ShopSystem.Pages
{
    public class ShopPageView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private PageTape _pageTape;
        
        private List<ShopCell> _cells = new List<ShopCell>();

        private void OnEnable()
        {
            StopCoroutine(ShowCellsAnimated());
            StartCoroutine(ShowCellsAnimated());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                StartCoroutine(Roll());
        }

        public void Initialize(ShopPage shopPage, ToggleGroup shopToggleGroup)
        {
            _cells.Clear();
        
            _pageTape.Initialize(shopPage.Name, shopPage.NameTextColor, shopPage.TapeColor);
        
            foreach (var unit in shopPage.Units)
            {
                var cell = DiContainerRef.Container.InstantiatePrefabForComponent<ShopCell>(shopPage.CellTemplate, _grid.transform);
                cell.Initialize(unit, shopToggleGroup);
                _cells.Add(cell);
                
                shopToggleGroup.AddToggle(cell.Toggle);
            }
        }

        public bool TryGetCell(Skin skin, out ShopCell foundCell)
        {
            foundCell = null;
            
            if (_cells == null || _cells.Count == 0)
                return false;
            
            foreach (var cell in _cells)
            {
                if (cell.ShopUnit.Skin == skin)
                {
                    foundCell = cell;
                    return true;
                }
            }

            return false;
        }

        private IEnumerator ShowCellsAnimated()
        {
            float interval = 0.05f;

            foreach (var cell in _cells)
            {
                yield return new WaitForSeconds(interval);
                cell.ShopCellView.Show();
            }
        }
        
        private IEnumerator Roll()
        {
            float maxInterval = 1f;
            float interval = 0.05f;
            float slowdown = 1.2f;
            
            var lockedCells = _cells.Where(cell => cell.IsOpened == false).ToArray();
            int index = 0;

            
            if (lockedCells.Length == 0)
                yield break;

            foreach (var cell in _cells)
                cell.IsClickable = false;
            

            while (interval < maxInterval)
            {
                DeselectAllExcept(lockedCells, index);
                lockedCells[index].ShopCellView.RouletteSelect();

                yield return new WaitForSeconds(interval);
                
                interval *= slowdown;
                
                if (interval < maxInterval)
                    index = index >= lockedCells.Length - 1 ? 0 : index + 1;    
            }

            DeselectAllExcept(lockedCells, index);
            lockedCells[index].ShopCellView.Unlock();
            lockedCells[index].ShopCellView.RouletteDeselect();
            
            foreach (var cell in _cells)
                cell.IsClickable = true;
        }

        private void DeselectAllExcept(ShopCell[] shopCells, int exceptionIndex)
        {
            for (int i = 0; i < shopCells.Length; i++)
            {
                if (i != exceptionIndex)
                    shopCells[i].ShopCellView.RouletteDeselect();
            }
        }
    }
}
