using UnityEngine;

namespace Shop.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private int _id;

        public int Id => _id;
    }
}
