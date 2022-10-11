using UnityEngine;

namespace ShopSystem.Items
{
    public class Skin : Item
    {
        [SerializeField] private Color _spiritColor;

        public Color SpiritColor => _spiritColor;
    }
}