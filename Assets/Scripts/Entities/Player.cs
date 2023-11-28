using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [SerializeField] private bool killable;
    private int _currencyTotal = 1000;
    public int CurrencyTotal => _currencyTotal;
    [SerializeField] private float pickupRadius;
    protected override void Start() {
        base.Start();
        _health.maxValue = 100;
        DontDestroyOnLoad(this);
    }
    public override void Die() {
        if (killable) {
            Destroy(gameObject);
            Destroy(GameObject.Find("UI"));
            SceneManager.LoadScene("DeathScene");
        }
    }

    void Update() {
        LayerMask mask = LayerMask.GetMask("Currency");
        Collider2D[] hits = Physics2D.OverlapCircleAll(gameObject.transform.position, pickupRadius, mask);
        foreach (Collider2D hit in hits) {
            var coin = hit.GetComponent<Coin>();
            coin?.PullTowards(gameObject.transform.position);
        }
    }
    public void CollectCoin(int coinValue) {
        _currencyTotal += coinValue;
    }
    public void SpendCoin(int coinValue)
    {
        _currencyTotal -= coinValue;
    }
}
