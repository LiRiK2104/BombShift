using ShopSystem.Items;
using UnityEngine;

namespace ShopSystem.Units
{
    public abstract class ShopUnit : ScriptableObject
    {
        [SerializeField] private Skin _skin;

        public Skin Skin => _skin;
    }
}
