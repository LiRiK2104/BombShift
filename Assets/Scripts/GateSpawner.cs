using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GateSpawner : MonoBehaviour
{
    [SerializeField] private List<Gate> _gateTemplates = new List<Gate>();

    public event Action GateSpawned;

    private Gate _lastSpawnedGate;
    

    public void Spawn(Transform spawnPoint)
    {
        if (spawnPoint == null)
            return;
        
        Instantiate(GetTemplate(), spawnPoint.position, Quaternion.identity, spawnPoint);
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
