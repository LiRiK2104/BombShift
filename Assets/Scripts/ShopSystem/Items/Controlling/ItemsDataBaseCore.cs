using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem.Items.Controlling
{
    [CreateAssetMenu(menuName = "Create ItemsDataBase", fileName = "ItemsDataBase", order = 67)]
    public class ItemsDataBaseCore : ScriptableObject
    {
        [SerializeField] private List<Item> _items = new List<Item>();

        public bool TryGetItem(int id, out Item foundItem)
        {
            foundItem = null;

            foreach (var item in _items)
            {
                if (item.Id == id)
                {
                    foundItem = item;
                    return true;
                }
            }

            return false;
        }
    }
}
