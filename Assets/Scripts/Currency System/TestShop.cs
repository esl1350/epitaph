using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TestShop : MonoBehaviour
{
    private float timer = 0f;
    public ItemManager manager;

    private GameObject cannotPurchaseTag;

    private GameObject purchaseSuccessTag;

    private GameObject inventoryFullTag;

    [SerializeField]
    public Item item1;
    private GameObject item1UI;
    private TextMeshProUGUI price1;
    private TextMeshProUGUI name1;
    private Image sprite1;

    public Item item2;
    private GameObject item2UI;
    private TextMeshProUGUI price2;
    private TextMeshProUGUI name2;
    private Image sprite2;

    public Item item3;
    private GameObject item3UI;
    private TextMeshProUGUI price3;
    private TextMeshProUGUI name3;
    private Image sprite3;

    public Item item4;
    private GameObject item4UI;
    private TextMeshProUGUI price4;
    private TextMeshProUGUI name4;

    private Image sprite4;

    public Item item5;
    private GameObject item5UI;
    private TextMeshProUGUI price5;
    private TextMeshProUGUI name5;

    private Image sprite5;

    public Item item6;
    private GameObject item6UI;
    private TextMeshProUGUI price6;
    private TextMeshProUGUI name6;

    private Image sprite6;

    void Awake()
    {
        cannotPurchaseTag = GameObject.FindWithTag("PurchaseFail");
        purchaseSuccessTag = GameObject.FindWithTag("PurchaseSuccess");
        inventoryFullTag = GameObject.FindWithTag("InventoryFull");
        item1UI = GameObject.FindWithTag("Item1UI");
        item2UI = GameObject.FindWithTag("Item2UI");
        item3UI = GameObject.FindWithTag("Item3UI");
        item4UI = GameObject.FindWithTag("Item4UI");
        item5UI = GameObject.FindWithTag("Item5UI");
        item6UI = GameObject.FindWithTag("Item6UI");
        sprite1 = item1UI.GetComponent<Image>();
        sprite2 = item2UI.GetComponent<Image>();
        sprite3 = item3UI.GetComponent<Image>();
        sprite4 = item4UI.GetComponent<Image>();
        sprite5 = item5UI.GetComponent<Image>();
        sprite6 = item6UI.GetComponent<Image>();
        price1 = item1UI.transform.Find("ItemAmount").gameObject.GetComponent<TextMeshProUGUI>();
        price2 = item2UI.transform.Find("ItemAmount").gameObject.GetComponent<TextMeshProUGUI>();
        price3 = item3UI.transform.Find("ItemAmount").gameObject.GetComponent<TextMeshProUGUI>();
        price4 = item4UI.transform.Find("ItemAmount").gameObject.GetComponent<TextMeshProUGUI>();
        price5 = item5UI.transform.Find("ItemAmount").gameObject.GetComponent<TextMeshProUGUI>();
        price6 = item6UI.transform.Find("ItemAmount").gameObject.GetComponent<TextMeshProUGUI>();
        name1 = item1UI.transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>();
        name2 = item2UI.transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>();
        name3 = item3UI.transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>();
        name4 = item4UI.transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>();
        name5 = item5UI.transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>();
        name6 = item6UI.transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>();
        price1.text = (item1.getCost()).ToString() + " coins";
        name1.text = item1.getName();
        sprite1.sprite = item1.getSprite();
        price2.text = (item2.getCost()).ToString() + " coins";
        name2.text = item2.getName();
        sprite2.sprite = item2.getSprite();
        price3.text = (item3.getCost()).ToString() + " coins";
        name3.text = item3.getName();
        sprite3.sprite = item3.getSprite();
        price4.text = (item4.getCost()).ToString() + " coins";
        name4.text = item4.getName();
        sprite4.sprite = item4.getSprite();
        price5.text = (item5.getCost()).ToString() + " coins";
        name5.text = item5.getName();
        sprite5.sprite = item5.getSprite();
        price6.text = (item6.getCost()).ToString() + " coins";
        name6.text = item6.getName();
        sprite6.sprite = item6.getSprite();
    }

    void Update()
    {
        if (purchaseSuccessTag.activeSelf){
            timer += Time.unscaledDeltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                DeactivatePurchaseSuccessTag();
            }
        }

        if (cannotPurchaseTag.activeSelf){
            timer += Time.unscaledDeltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                DeactivateCannotPurchaseTag();
            }
        }
        if (inventoryFullTag.activeSelf){
            timer += Time.unscaledDeltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                inventoryFullTag.SetActive(false);
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
            Invoke("DeactivatePurchaseSuccessTag", 3);
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3);
        }
    }

    public void BuyItem2(){
        manager.PurchaseItem(item2);
        bool itemExist = manager.doesItemExist(item2);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item2UI.SetActive(false);
            Invoke("DeactivatePurchaseSuccessTag", 3);
            //DeactivatePurchaseSuccessTag();
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3);
            //DeactivateCannotPurchaseTag();
        }
    }

    public void BuyItem3(){
        manager.PurchaseItem(item3);
        bool itemExist = manager.doesItemExist(item3);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item3UI.SetActive(false);
            Invoke("DeactivatePurchaseSuccessTag", 3);
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3);
        }
    }

    public void BuyItem4(){
        manager.PurchaseItem(item4);
        bool itemExist = manager.doesItemExist(item4);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item4UI.SetActive(false);
            Invoke("DeactivatePurchaseSuccessTag", 3);
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3);
        }
    }

    public void BuyItem5(){
        manager.PurchaseItem(item5);
        bool itemExist = manager.doesItemExist(item5);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item5UI.SetActive(false);
            Invoke("DeactivatePurchaseSuccessTag", 3);
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3);
        }
    }

    public void BuyItem6(){
        manager.PurchaseItem(item6);
        bool itemExist = manager.doesItemExist(item6);
        if (itemExist == true){
            Debug.Log("purchase success");
            purchaseSuccessTag.SetActive(true);
            item6UI.SetActive(false);
            Invoke("DeactivatePurchaseSuccessTag", 3);
        }
        else{
            Debug.Log("cannot purchase");
            cannotPurchaseTag.SetActive(true);
            Invoke("DeactivateCannotPurchaseTag", 3);
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

    void waiter()
    {
        Debug.Log("waiting to disable UI");
    }
}
