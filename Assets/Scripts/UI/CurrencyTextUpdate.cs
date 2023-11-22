using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyTextUpdate : MonoBehaviour
{
    private Player player;
    private TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
       tmp = this.GetComponent<TextMeshProUGUI>();
       tmp.SetText("Coins: " + player.CurrencyTotal);
    }

    // Update is called once per frame
    void Update()
    {
        tmp.SetText("Coins: " + player.CurrencyTotal);
    }
}
