using ShopSystem;
using UnityEngine;
using Zenject;

namespace Progress
{
    public class ProgressController : MonoBehaviour
    {
        [Inject] private Inventory _inventory;
        
        public void StartOver()
        {
            _inventory.DeleteAll();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
