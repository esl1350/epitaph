using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthSlider : MonoBehaviour
{
    private Entity entityToTrack;
    private Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        entityToTrack = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.maxValue = entityToTrack.Health.intialValue;
        _slider.value = entityToTrack.HealthVal;
    }
}
