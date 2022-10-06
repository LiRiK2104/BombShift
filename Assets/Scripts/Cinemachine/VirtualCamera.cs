using System.Collections;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Cinemachine
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class VirtualCamera : MonoBehaviour
    {
        private const float DefaultShakeTime = 0.5f;
        private const float DefaultShakeIntensity = 1;
        
        [Inject] private Player _player;
    
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
        private CinemachineTransposer _cinemachineTransposer;
    
        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        
            _cinemachineBasicMultiChannelPerlin = 
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
            _cinemachineTransposer = 
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }
    
        public void StartShake(float time = DefaultShakeTime, float intensity = DefaultShakeIntensity)
        {
            StartCoroutine(ShakeCore(time, intensity));
        }

        public void StopFollowing()
        {
            _cinemachineVirtualCamera.Follow = null;
        }

        public void StopLookingAt()
        {
            _cinemachineVirtualCamera.LookAt = null;
        }

        public void UpdateSpeedEffect()
        {
            _cinemachineTransposer.m_ZDamping = _player.SpeedSwitcher.Setting.CameraDistance;
        }

        private IEnumerator ShakeCore(float time, float intensity)
        {
            time = Mathf.Max(time, 0); 
        
            if (time == 0)
                yield break;

            float halfYime = time / 2;
            float minIntensity = 0;

            yield return ShakeOverTime(halfYime, minIntensity, intensity);
            yield return ShakeOverTime(halfYime, intensity, minIntensity);
        }

        private IEnumerator ShakeOverTime(float targetTime, float startIntensity, float endIntensity)
        {
            float timer = 0;
        
            while (timer < targetTime)
            {
                timer += Time.deltaTime;
            
                float intensity = Mathf.Lerp(startIntensity, endIntensity, timer / targetTime);
                Shake(intensity);
            
                yield return null;
            }
        }

        private void Shake(float intensity)
        {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        }
    }
}
