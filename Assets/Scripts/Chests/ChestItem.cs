using ShopSystem.Items;
using UnityEngine;

namespace Chests
{
    public class ChestItem : MonoBehaviour
    {
        private Item _item;

        public Item Item => _item;

        public void Initialize(Item itemTemplate)
        {
            _item = Instantiate(itemTemplate, transform.position, Quaternion.identity);
        }
    }
}
