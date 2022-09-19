using System;
using UnityEngine;

namespace Shop.Toggles
{
    public class Toggle : MonoBehaviour
    {
        public event Action Activating;
        public event Action Deactivating;

        public void Select()
        {
            Activating?.Invoke();
        }
    
        public void Deselect()
        {
            Deactivating?.Invoke();
        }
    }
}
