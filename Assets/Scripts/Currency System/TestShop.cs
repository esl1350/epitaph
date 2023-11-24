using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShop : MonoBehaviour
{
    private ItemManager manager;

    [SerializeField]
    public Item item1;

    public Item item2;

    public Item item3;

    public Item item4;

    public Item item5;

    public Item item6;

    void Awake()
    {
        manager = gameObject.GetComponent<ItemManager>();
    }

    void Update()
    {

    }

    public void BuyItem1(){
        manager.PurchaseItem(item1);
    }

    public void BuyItem2(){
        manager.PurchaseItem(item2);
    }

    public void BuyItem3(){
        manager.PurchaseItem(item3);
    }

    public void BuyItem4(){
        manager.PurchaseItem(item4);
    }

    public void BuyItem5(){
        manager.PurchaseItem(item5);
    }

    public void BuyItem6(){
        manager.PurchaseItem(item6);
    }
}
