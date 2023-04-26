using PlayerLogic;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace FX
{
    public class GateSpiritEffect : Effect
    {
        [SerializeField] private Mesh _mesh;
        
        [Inject] private Player _player;
        [Inject] private SkinStorage _skinStorage;

        
        private void OnEnable()
        {
            _player.SkinSetter.SkinSetted += SetColor;
        }

        private void OnDisable()
        {
            _player.SkinSetter.SkinSetted -= SetColor;
        }

        private void Start()
        {
            SetColor(_skinStorage.SavedSkin);
            ParticleSystem.GetComponent<ParticleSystemRenderer>().mesh = _mesh;
        }
        

        private void SetColor(Skin skin)
        {
            var mainModule = ParticleSystem.main;
            mainModule.startColor = skin.SpiritColor;
        }
    }
}
