using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HealthTextUpdate : MonoBehaviour
{

    [SerializeField]private Entity entityToTrack;

    [SerializeField]private bool trackPlayer = false;
    private TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
       if (trackPlayer)
        entityToTrack = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        tmp = this.GetComponent<TextMeshProUGUI>();
        tmp.SetText(entityToTrack.HealthVal.ToString("F1") + "/" + entityToTrack.Health.intialValue);
    }

    // Update is called once per frame
    void Update()
    {
        tmp.SetText(entityToTrack.HealthVal.ToString("F1") + "/" + entityToTrack.Health.intialValue);
    }
}
