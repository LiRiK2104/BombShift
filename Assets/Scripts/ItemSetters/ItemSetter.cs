using System.Collections.Generic;
using ShopSystem;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace ItemSetters
{
    public class ItemSetter<T> : MonoBehaviour where T : Item
    {
        [SerializeField] private Transform _itemSpawnPoint;
        
        [Inject] protected Inventory Inventory;
        
        private List<T> _createdItems = new List<T>();


        protected void SetItem(T item)
        {
            DisableAllCreatedItems();

            if (TryGetCreatedItem(item, out T foundItem))
                foundItem.gameObject.SetActive(true);
            else
                _createdItems.Add(CreateItem(item));
        }

        private bool TryGetCreatedItem(T needItem, out T foundItem)
        {
            foundItem = null;

            foreach (var createdSkin in _createdItems)
            {
                if (createdSkin.Id == needItem.Id)
                {
                    foundItem = createdSkin;
                    return true;
                }
            }

            return false;
        }

        protected virtual T CreateItem(T fragmentPrefab)
        {
            return Instantiate(fragmentPrefab, _itemSpawnPoint);
        }
        
        private void DisableAllCreatedItems()
        {
            foreach (var createdItem in _createdItems)
                createdItem.gameObject.SetActive(false);
        }
    }
}
