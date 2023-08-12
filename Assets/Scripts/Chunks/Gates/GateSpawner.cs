using System;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Chunks.Gates
{
    public class GateSpawner : MonoBehaviour
    {
        [SerializeField] private List<Gate> _gateTemplates = new List<Gate>();

        public event Action GateSpawned;

        private Gate _lastSpawnedGate;
        private DiContainer _container;


        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }
        
        
        public void Spawn(Transform spawnPoint)
        {
            if (spawnPoint == null)
                return;
        
            _container.InstantiatePrefab(GetTemplate(), spawnPoint.position, Quaternion.identity, spawnPoint);
            GateSpawned?.Invoke();
        }

        private Gate GetTemplate()
        {
            Gate template;

            do
            {
                int index = Random.Range(0, _gateTemplates.Count);
                template = _gateTemplates[index];
            } 
            while (_lastSpawnedGate == template);

            _lastSpawnedGate = template;
            return template;
        }
    }
}
