using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounterUpdate : MonoBehaviour
{
    private GameObject enemyManager;
    private TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.FindWithTag("EnemyManager");
       tmp = this.GetComponent<TextMeshProUGUI>();
       tmp.SetText(enemyManager.transform.childCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
       if(enemyManager == null) {
         enemyManager = GameObject.FindWithTag("EnemyManager");
       }
       tmp.SetText(enemyManager.transform.childCount.ToString());
    }
}
