using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShop : MonoBehaviour
{
    private ItemManager manager;

    [SerializeField]
    private Item item;

    void Awake()
    {
        manager = gameObject.GetComponent<ItemManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Attempting to Purchase...");
            manager.PurchaseItem(item);
        }
    }
}
