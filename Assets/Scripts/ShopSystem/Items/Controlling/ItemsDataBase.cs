using Helpers;
using UnityEngine;

namespace ShopSystem.Items.Controlling
{
    public class ItemsDataBase : Singleton<ItemsDataBase>
    {
        [SerializeField] private ItemsDataBaseCore _core;

        public ItemsDataBaseCore Core => _core;
    }
}
