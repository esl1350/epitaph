using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TestShop : MonoBehaviour
{
    private float timer = 0f;
    public ItemManager manager;

    public GameObject cannotPurchaseTag;

    public GameObject purchaseSuccessTag;

    [SerializeField]
    public Item item1;
    public GameObject item1UI;
    public TextMeshProUGUI price1;
    public TextMeshProUGUI name1;

    public Item item2;
    public GameObject item2UI;
    public TextMeshProUGUI price2;
    public TextMeshProUGUI name2;

    public Item item3;
    public GameObject item3UI;
    public TextMeshProUGUI price3;
    public TextMeshProUGUI name3;

    public Item item4;
    public GameObject item4UI;
    public TextMeshProUGUI price4;
    public TextMeshProUGUI name4;

    public Item item5;
    public GameObject item5UI;
    public TextMeshProUGUI price5;
    public TextMeshProUGUI name5;

    public Item item6;
    public GameObject item6UI;
    public TextMeshProUGUI price6;
    public TextMeshProUGUI name6;

    void Awake()
    {
        manager = GameObject.FindWithTag("Player").GetComponent<ItemManager>();
        price1.text = (item1.getCost()).ToString() + " coins";
        name1.text = item1.getName();
        price2.text = (item2.getCost()).ToString() + " coins";
        name2.text = item2.getName();
        price3.text = (item3.getCost()).ToString() + " coins";
        name3.text = item3.getName();
        price4.text = (item4.getCost()).ToString() + " coins";
        name4.text = item4.getName();
        price5.text = (item5.getCost()).ToString() + " coins";
        name5.text = item5.getName();
        price6.text = (item6.getCost()).ToString() + " coins";
        name6.text = item6.getName();
    }

    void Update()
    {
        if (purchaseSuccessTag.activeSelf){
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                DeactivatePurchaseSuccessTag();
            }
        }

        if (cannotPurchaseTag.activeSelf){
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                DeactivateCannotPurchaseTag();
            }
        }
    }

    public void BuyItem1(){
        Debug.Log("buyingItem1");
        manager.PurchaseItem(item1);
        bool itemExist = manager.doesItemExist(item1);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item1UI.SetActive(false);
            DeactivatePurchaseSuccessTag();
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            DeactivateCannotPurchaseTag();
        }
    }

    public void BuyItem2(){
        manager.PurchaseItem(item2);
        bool itemExist = manager.doesItemExist(item2);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item2UI.SetActive(false);
            Invoke("DeactivatePurchaseSuccessTag", 3f);
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3f);
        }
    }

    public void BuyItem3(){
        manager.PurchaseItem(item3);
        bool itemExist = manager.doesItemExist(item3);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item3UI.SetActive(false);
            Invoke("DeactivatePurchaseSuccessTag", 3f);
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3f);
        }
    }

    public void BuyItem4(){
        manager.PurchaseItem(item4);
        bool itemExist = manager.doesItemExist(item4);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item4UI.SetActive(false);
            Invoke("DeactivatePurchaseSuccessTag", 3f);
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3f);
        }
    }

    public void BuyItem5(){
        manager.PurchaseItem(item5);
        bool itemExist = manager.doesItemExist(item5);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item5UI.SetActive(false);
            Invoke("DeactivatePurchaseSuccessTag", 3f);
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3f);
        }
    }

    public void BuyItem6(){
        manager.PurchaseItem(item6);
        bool itemExist = manager.doesItemExist(item6);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item6UI.SetActive(false);
            Invoke("DeactivatePurchaseSuccessTag", 3f);
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3f);
        }
    }

    private void DeactivateCannotPurchaseTag()
    {
        cannotPurchaseTag.SetActive(false);
    }

    private void DeactivatePurchaseSuccessTag()
    {
        purchaseSuccessTag.SetActive(false);
    }
}
