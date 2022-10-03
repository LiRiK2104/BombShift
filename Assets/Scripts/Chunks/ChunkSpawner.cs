using System;
using System.Collections.Generic;
using Chunks.Gates;
using Helpers;
using UnityEngine;

public class ChunkSpawner : Singleton<ChunkSpawner>
{
    private const int MinChunksCount = 5;
    private const int MaxChunksCount = 30;

    [SerializeField] private GateSpawner _gateSpawner;
    [SerializeField] [Range(MinChunksCount, MaxChunksCount)] private int _chunksCount;
    [SerializeField] private List<ProbabilityCell<Chunk>> _chunkTemplates = new List<ProbabilityCell<Chunk>>();
    [SerializeField] private StartChunk _startChunk;
    [SerializeField] private FinishChunk _finishChunk;
    [SerializeField] private Transform _chunksContainer;
    
    private Roulette _roulette;

    public GateSpawner GateSpawner => _gateSpawner;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Transform spawnPoint = _startChunk.EndPoint;

        for (int i = 0; i < _chunksCount; i++)
        {
            var template = GetRandomTemplate();
            
            if (template == null)
                continue;
            
            var chunk = Instantiate(template, spawnPoint.position, Quaternion.identity, _chunksContainer);
            _gateSpawner.Spawn(chunk.GateSpawnPoint);
            
            spawnPoint = chunk.EndPoint;
        }

        _finishChunk.transform.position = spawnPoint.position;
        _finishChunk.transform.rotation = Quaternion.identity;
    }

    private Chunk GetRandomTemplate()
    {
        if (_roulette == null)
            InitializeRoulette();
        
        int index = _roulette.Roll();
        
        if (index >= 0 && index < _chunkTemplates.Count)
            return _chunkTemplates[index].Item;

        return null;
    }
    
    private void InitializeRoulette()
    {
        var probabilities = new Dictionary<int, double>();
        
        for (int i = 0; i < _chunkTemplates.Count; i++)
            probabilities.Add(i, _chunkTemplates[i].Probability);

        _roulette = new Roulette(probabilities);
    }
}
