using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HealthTextUpdate : MonoBehaviour
{

    [SerializeField]private Entity entityToTrack;
    private TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
       tmp = this.GetComponent<TextMeshProUGUI>();
       tmp.SetText("Health: " + entityToTrack.HealthVal);
    }

    // Update is called once per frame
    void Update()
    {
        tmp.SetText("Health: " + entityToTrack.HealthVal);
    }
}
