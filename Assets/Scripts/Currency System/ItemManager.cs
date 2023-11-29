using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    private float timer = 0f;
    private Player player;
    public AbilityInventoryManager abilityInventory;
    private List<Item> items = new List<Item>();

    public AbilityWrapper ability;

    private GameObject inventoryFullTag;

    public void Awake()
    {
        inventoryFullTag = GameObject.FindWithTag("InventoryFull");
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

void Update()
    {
        if (inventoryFullTag.activeSelf){
            timer += Time.unscaledDeltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                inventoryFullTag.SetActive(false);
            }
        }
    }

    public void PurchaseItem(Item itemToBuy)
    {
        if (player.CurrencyTotal < itemToBuy.getCost())
        {
            int difference = itemToBuy.getCost() - player.CurrencyTotal;
            Debug.Log("Need " + difference + " more coins.");
        } 
        else
        {
            bool isItFull = abilityInventory.Add(ability);
            bool remove = abilityInventory.Remove(ability);
            if (itemToBuy.isAbility() == false || isItFull == true){
                player.SpendCoin(itemToBuy.getCost());
                itemToBuy.disable();
                itemToBuy.activate(player);
                addItem(itemToBuy);
            }
            else{
                Debug.Log("No space in ability inventory");

                inventoryFullTag.SetActive(true);
            }
        }
    }

    private void addItem(Item item)
    {
        items.Add(item);
    }

    public void removeItem(Item item)
    {
        items.Remove(item);
    }

    public void dealBuffedDamage(float damageDealt, Entity current, Entity target)
    {
        foreach (Item item in items) {
            current.DealDamageAugmented(target, item.applyItemDamageDealt(damageDealt, current, target));
        }
    }

    public bool doesItemExist(Item item)
    {
        return items.Contains(item);
    }

}
