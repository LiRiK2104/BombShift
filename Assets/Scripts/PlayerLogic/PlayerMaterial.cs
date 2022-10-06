using System.Collections;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(typeof(Renderer))]
    public class PlayerMaterial : MonoBehaviour
    {
        private const string EmissionColorID = "_EmissionColor";
        private Renderer _renderer;
        private Player _player;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            _player.LifeSwitcher.LifeChanged += UpdateEmissionEntry;
        }

        private void OnDisable()
        {
            _player.LifeSwitcher.LifeChanged -= UpdateEmissionEntry;
        }

        public void Initialize(Player player)
        {
            _player = player;
        }

        private void UpdateEmissionEntry()
        {
            StopCoroutine(UpdateEmission());
            StartCoroutine(UpdateEmission());
        }

        private IEnumerator UpdateEmission()
        {
            Color startColor = _renderer.material.GetColor(EmissionColorID);
            Color targetColor = _player.LifeSwitcher.Setting.EmissionColor;

            if (startColor == targetColor)
                yield break;
        
            float progress = 0;
            float speed = 2;
        
            while (progress < 1)
            {
                progress += Time.deltaTime * speed;
                Color stepColor = Color.Lerp(startColor, targetColor, progress);
                _renderer.material.SetColor(EmissionColorID, stepColor);

                yield return null;
            }
        }
    }
}
