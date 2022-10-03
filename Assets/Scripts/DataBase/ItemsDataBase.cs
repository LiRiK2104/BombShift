using Helpers;
using UnityEngine;

namespace DataBase
{
    public class ItemsDataBase : Singleton<ItemsDataBase>
    {
        [SerializeField] private ItemsDataBaseCore _core;

        public ItemsDataBaseCore Core => _core;
    }
}
