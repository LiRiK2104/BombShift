using System.Collections.Generic;
using ShopSystem.Cells;
using ShopSystem.Units;
using UnityEngine;

namespace ShopSystem.Pages
{
    public abstract class Page : ScriptableObject
    {
        protected const int MaxUnitsCount = 6;

        [SerializeField] private string _name;
        [SerializeField] private Color _nameTextColor;
        [SerializeField] private Color _tapeColor;
    
        public abstract Cell CellTemplate { get; }
        public abstract List<Unit> Units { get; }

        public string Name => _name;
        public Color NameTextColor => _nameTextColor;
        public Color TapeColor => _tapeColor;
    }
}
