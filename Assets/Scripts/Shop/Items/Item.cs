using UnityEngine;

namespace Shop.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
    
        public Sprite Icon => _icon;
    }
}
