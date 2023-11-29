using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    private float shakeIntensity = 1f;
    private float shakeTime = 0.2f;
    private CinemachineBasicMultiChannelPerlin perlin;
    // Start is called before the first frame update
    void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }
    void Start() {
        StopShake();
    }

    public void Shake(float intensity, float time) {
        StopAllCoroutines();
        perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intensity;
        StartCoroutine(StartShake(time));
    }

    public void Shake() {
        StopAllCoroutines();
        perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = shakeIntensity;
        StartCoroutine(StartShake(shakeTime));

    }
    void StopShake() {
        perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0f;
    }
    IEnumerator StartShake(float time) {
        yield return new WaitForSeconds(time);
        StopShake();

    }
}
