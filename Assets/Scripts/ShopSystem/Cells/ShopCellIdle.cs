using ShopSystem.Cells.States;
using UnityEngine;

namespace ShopSystem.Cells
{
    public class ShopCellIdle : ShopCell
    {
        [SerializeField] private LockedState _lockedState;
    
        protected override LockedState LockedState => _lockedState;
    }
}
