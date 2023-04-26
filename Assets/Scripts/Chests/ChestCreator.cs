using UnityEngine;

namespace Chests
{
    public class ChestCreator : MonoBehaviour
    {
        [SerializeField] private Transform _chestPoint;
    
        private Chest _chest;

        public Chest Chest => _chest;

        public void CreateChest(Chest chestTemplate)
        {
            _chest = Instantiate(chestTemplate, _chestPoint.position, _chestPoint.rotation, _chestPoint);
        }
    }
}