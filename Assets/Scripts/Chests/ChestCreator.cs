using System.Collections.Generic;
using UnityEngine;

namespace Chests
{
    public class ChestCreator : MonoBehaviour
    {
        [SerializeField] private List<Chest> _chestsTemplates = new List<Chest>();
        [SerializeField] private int _chestIndex;
        [Space]
        [SerializeField] private Transform _chestPoint;
    
        private Chest _chest;

        public Chest Chest => _chest;

        private void Awake()
        {
            CreateChest();
        }

        private void OnValidate()
        {
            _chestIndex = Mathf.Clamp(_chestIndex, 0, _chestsTemplates.Count - 1);
        }

        private void CreateChest()
        {
            _chest = Instantiate(_chestsTemplates[_chestIndex], _chestPoint.position, _chestPoint.rotation, _chestPoint);
        }
    }
}