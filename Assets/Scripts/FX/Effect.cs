using UnityEngine;

namespace FX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Effect : MonoBehaviour, IWasCreatedFrom<Effect>
    {
        private ParticleSystem _particleSystem;
    
        public Effect Prefab { get; private set; }
        protected ParticleSystem ParticleSystem => _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void Play()
        {
            _particleSystem.Play();
        }
    
        public void Stop()
        {
            _particleSystem.Stop();
        }
    
        public void OnCreate(Effect prefab)
        {
            Prefab = prefab;
        }
    }
}
