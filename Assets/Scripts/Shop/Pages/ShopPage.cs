using System.Collections.Generic;
using Shop.Cells;
using Shop.Units;
using UnityEngine;

namespace Shop.Pages
{
    public abstract class ShopPage : ScriptableObject
    {
        protected const int MaxUnitsCount = 6;

        [SerializeField] private string _name;
        [SerializeField] private Color _nameTextColor;
        [SerializeField] private Color _tapeColor;
    
        public abstract ShopCell CellTemplate { get; }
        public abstract List<ShopUnit> Units { get; }

        public string Name => _name;
        public Color NameTextColor => _nameTextColor;
        public Color TapeColor => _tapeColor;
    }
}
