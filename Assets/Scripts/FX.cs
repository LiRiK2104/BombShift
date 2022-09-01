using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FX : MonoBehaviour, IWasCreatedFrom<FX>
{
    private ParticleSystem _effect;
    
    public FX Prefab { get; private set; }

    private void Awake()
    {
        _effect = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        _effect.Play();
    }
    
    public void Stop()
    {
        _effect.Stop();
    }
    
    public void OnCreate(FX prefab)
    {
        Prefab = prefab;
    }
}
