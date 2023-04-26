using System;
using System.Linq;
using Chunks;
using Chunks.Gates;
using RoundLogic;
using ShopSystem;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace PlayerLogic.Spirit
{
    [RequireComponent(typeof(Player))]
    public class SpiritSetter : MonoBehaviour
    {
        [SerializeField] private Spirit _spiritQuad;
        [SerializeField] private SpiritBox _spiritBox;

        [Inject] private ChunkSpawner _chunkSpawner;
        [Inject] private RoundRunner _roundRunner;

        private Player _player;
        
        public event Action<Vector3> SpiritPointFound;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            _player.SkinSetter.SkinSetted += SetColor;
            Gate.Passed += SetSpirit;
            _roundRunner.Starter.RoundStarted += SetSpirit;
            _chunkSpawner.GateSpawner.GateSpawned += SetSpirit;
            _player.Died += Hide;
        }

        private void OnDisable()
        {
            _player.SkinSetter.SkinSetted -= SetColor;
            Gate.Passed -= SetSpirit;
            _roundRunner.Starter.RoundStarted -= SetSpirit;
            _chunkSpawner.GateSpawner.GateSpawned -= SetSpirit;
            _player.Died -= Hide;
        }

        private void Start()
        {
            Hide();
        }

        private void Show()
        {
            if (_player.Alive == false)
                return;
        
            _spiritBox.gameObject.SetActive(true);
            _spiritQuad.gameObject.SetActive(true);
        }
    
        private void Hide()
        {
            _spiritQuad.gameObject.SetActive(false);
            _spiritBox.gameObject.SetActive(false);
        }

        private void SetSpirit()
        {
            if (TryFindSpiritPoint(out Transform spiritPoint))
            {
                Show();
                _spiritQuad.UpdateTargetPosition(spiritPoint.position);
            }
            else
            {
                Hide();
            }
        }
        
        private void SetColor(Skin skin)
        {
            float quadAlpha = 0.5f;
            float boxAlpha = 0.25f;

            _spiritQuad.Color = new Color(skin.SpiritColor.r, skin.SpiritColor.g, skin.SpiritColor.b, quadAlpha);
            _spiritBox.Color = new Color(skin.SpiritColor.r, skin.SpiritColor.g, skin.SpiritColor.b, boxAlpha);
        }

        private bool TryFindSpiritPoint(out Transform point)
        {
            point = null;

            float sideHalfSize = 3;
            float searchDistance = 100;
            int layerMask = LayerMask.GetMask("Spirit Point");
            Vector3 boxHalfSize = new Vector3(sideHalfSize, sideHalfSize, sideHalfSize);

            RaycastHit[] hits = Physics.BoxCastAll(transform.position, boxHalfSize, 
                Vector3.forward, Quaternion.identity, searchDistance, layerMask);

            if (hits.Length > 0)
            {
                RaycastHit nearestHit = hits.OrderBy(hit => hit.distance).First();
                point = nearestHit.transform;
                SpiritPointFound?.Invoke(point.position);
                return true;
            }

            return false;
        }
    }
}
