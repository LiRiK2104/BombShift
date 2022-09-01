using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class VirtualCameraHandler : Singleton<VirtualCameraHandler>
{
    private const float DefaultShakeTime = 0.5f;
    private const float DefaultShakeIntensity = 1;

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

    private void OnEnable()
    {
        SpeedSetter.Instance.SpeedChanged += UpdateSpeedEffect;
        Player.Instance.Died += StopFollowing;
        Player.Instance.Died += StopLookingAt;
    }

    private void OnDisable()
    {
        SpeedSetter.Instance.SpeedChanged -= UpdateSpeedEffect;
        Player.Instance.Died -= StopFollowing;
        Player.Instance.Died -= StopLookingAt;
    }

    public void StartShake(float time = DefaultShakeTime, float intensity = DefaultShakeIntensity)
    {
        StartCoroutine(ShakeCore(time, intensity));
    }
    
    public void SlowDown()
    {
        StartCoroutine(SlowDownCore());
    }

    private void StopFollowing()
    {
        _cinemachineVirtualCamera.Follow = null;
    }

    private void StopLookingAt()
    {
        _cinemachineVirtualCamera.LookAt = null;
    }

    private void UpdateSpeedEffect()
    {
        _cinemachineTransposer.m_ZDamping = SpeedSetter.Instance.Setting.CameraDistance;
    }

    private IEnumerator SlowDownCore()
    {
        float maxDamping = 20;
        float speed = 1.5f;
        
        while (_cinemachineTransposer.m_ZDamping < maxDamping)
        {
            _cinemachineTransposer.m_ZDamping = Mathf.MoveTowards(_cinemachineTransposer.m_ZDamping, 
                maxDamping, Time.deltaTime * speed);

            yield return null;
        }
        
        StopFollowing();
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
