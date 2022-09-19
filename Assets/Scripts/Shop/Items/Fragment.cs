using UnityEngine;

namespace Shop.Items
{
    public class Fragment : Currency
    {
        [SerializeField] private Sprite _icon;
    
        public Sprite Icon => _icon;
    }
}
