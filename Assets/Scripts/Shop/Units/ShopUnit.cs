using Shop.Items;
using UnityEngine;

namespace Shop.Units
{
    public abstract class ShopUnit : ScriptableObject
    {
        [SerializeField] private Skin _skin;

        public Skin Skin => _skin;
    }
}
