using ShopSystem.Cells.States;
using UnityEngine;

namespace ShopSystem.Cells
{
    public class CellIdle : Cell
    {
        [SerializeField] private LockedState _lockedState;
    
        protected override LockedState LockedState => _lockedState;
    }
}
