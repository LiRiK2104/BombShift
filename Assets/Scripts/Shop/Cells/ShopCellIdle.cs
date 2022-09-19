using Shop.Cells.States;
using UnityEngine;

namespace Shop.Cells
{
    public class ShopCellIdle : ShopCell
    {
        [SerializeField] private LockedState _lockedState;
    
        protected override LockedState LockedState => _lockedState;
    }
}
