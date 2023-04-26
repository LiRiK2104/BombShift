using ShopSystem.Items;
using UnityEngine;

namespace ShopSystem.Units
{
    public abstract class Unit : ScriptableObject
    {
        [SerializeField] private Skin _skin;

        public Skin Skin => _skin;
    }
}
