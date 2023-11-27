using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightPulse : MonoBehaviour
{
    [SerializeField] private float lowestIntensity;
    [SerializeField] private float highestIntensity;
    [SerializeField] private float pulseSpeed;
    private UnityEngine.Rendering.Universal.Light2D light;
    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }
    // Update is called once per frame
    void Update()
    {
        light.intensity = lowestIntensity + Mathf.PingPong(Time.time * pulseSpeed, highestIntensity - lowestIntensity);
    }
}
