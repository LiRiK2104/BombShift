using System.Collections;
using System.Collections.Generic;
using Helpers;
using ShopSystem.Cells;
using ShopSystem.Items;
using UnityEngine;
using UnityEngine.UI;
using ToggleGroup = ShopSystem.Toggles.ToggleGroup;

namespace ShopSystem.Pages
{
    public class PageView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private PageTape _pageTape;

        protected List<Cell> Cells = new List<Cell>();

        
        protected void OnEnable()
        {
            StopCoroutine(PlayCellsShowAnimation());
            StartCoroutine(PlayCellsShowAnimation());
        }

        public virtual void Initialize(Page page, ToggleGroup shopToggleGroup)
        {
            Cells.Clear();
        
            _pageTape.Initialize(page.Name, page.NameTextColor, page.TapeColor);
        
            foreach (var unit in page.Units)
            {
                var cell = DiContainerRef.Container.InstantiatePrefabForComponent<Cell>(page.CellTemplate, _grid.transform);
                cell.Initialize(unit, shopToggleGroup);
                Cells.Add(cell);
                
                shopToggleGroup.AddToggle(cell.Toggle);
            }
        }

        public bool TryGetCell(Skin skin, out Cell foundCell)
        {
            foundCell = null;
            
            if (Cells == null || Cells.Count == 0)
                return false;
            
            foreach (var cell in Cells)
            {
                if (cell.Unit.Skin == skin)
                {
                    foundCell = cell;
                    return true;
                }
            }

            return false;
        }

        private IEnumerator PlayCellsShowAnimation()
        {
            float interval = 0.05f;

            foreach (var cell in Cells)
            {
                yield return new WaitForSeconds(interval);
                cell.CellView.Show();
            }
        }
    }
}
