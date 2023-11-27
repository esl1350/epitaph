using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Default coin value is SmallValue
    public int coinValue = (int) CoinValueEnum.SmallValue;
    // Colors of the different coin values
    public Sprite SMALL_VALUE_COIN_SPRITE;
    public Sprite MEDIUM_VALUE_COIN_SPRITE;
    public Sprite LARGE_VALUE_COIN_SPRITE;

    public float pullSpd;

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            var player = other.GetComponent<Player>();
            if (player != null) {
                player.CollectCoin(coinValue);
            }
             Destroy(this.gameObject);
        }
    }
    public void PullTowards(Vector3 pos) {
        gameObject.transform.position = gameObject.transform.position + pullSpd * Vector3.Normalize(pos - gameObject.transform.position) * Time.deltaTime;
    }

    public void setCoinValue(int value) {
        coinValue = value;
    }

}

public enum CoinValueEnum
{
    SmallValue = 5,
    MediumValue = 20,
    LargeValue = 50
}