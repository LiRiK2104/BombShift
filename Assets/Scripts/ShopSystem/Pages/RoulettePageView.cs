using System.Collections;
using System.Linq;
using ShopSystem.Cells;
using ShopSystem.Toggles;
using ShopSystem.Units;
using UI;
using UnityEngine;
using Zenject;

namespace ShopSystem.Pages
{
    public class RoulettePageView : PageView
    {
        [Inject] private TouchBlocker _touchBlocker;
        [Inject] private Shop _shop;

        private bool _isRolling;
        private PageRoulette _pageRoulette;

        public override void Initialize(Page page, ToggleGroup shopToggleGroup)
        {
            base.Initialize(page, shopToggleGroup);

            if (page is PageRoulette pageRoulette)
                _pageRoulette = pageRoulette;
        }

        public void StartRoll()
        {
            StartCoroutine(Roll());
        }
        
        private IEnumerator Roll()
        {
            float maxInterval = 1f;
            float interval = 0.05f;
            float slowdown = 1.2f;
            var lockedCells = Cells.Where(cell => cell.IsOpened == false).ToArray();
            int index = 0;

            
            if (_isRolling || lockedCells.Length == 0)
                yield break;

            _isRolling = true;
            _touchBlocker.Enable();

            if (lockedCells.Length > 1)
            {
                while (interval < maxInterval)
                {
                    DeselectAllExcept(lockedCells, index);
                    lockedCells[index].CellView.RouletteSelect();

                    yield return new WaitForSeconds(interval);
                
                    interval *= slowdown;
                
                    if (interval < maxInterval)
                        index = index >= lockedCells.Length - 1 ? 0 : index + 1;    
                }   
            }

            DeselectAllExcept(lockedCells, index);

            var prizeCell = lockedCells[index];
            prizeCell.CellView.Unlock();
            prizeCell.CellView.RouletteDeselect();
            BuyPrizeUnit(prizeCell.Unit);

            _isRolling = false;
            _touchBlocker.Disable();
        }

        private void BuyPrizeUnit(Unit unit)
        {
            if (unit is UnitIdle prizeUnit)
                _shop.Buy(prizeUnit, _pageRoulette);
        }

        private void DeselectAllExcept(Cell[] shopCells, int exceptionIndex)
        {
            for (int i = 0; i < shopCells.Length; i++)
            {
                if (i != exceptionIndex)
                    shopCells[i].CellView.RouletteDeselect();
            }
        }
    }
}
