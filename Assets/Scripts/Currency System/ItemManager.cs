using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private Player player;
    public AbilityInventoryManager abilityInventory;
    private List<Item> items = new List<Item>();

    public void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
            int len = abilityInventory.getAbilitiesLength();
            if (itemToBuy.isAbility() == false || len < 6){
                player.SpendCoin(itemToBuy.getCost());
                itemToBuy.disable();
                itemToBuy.activate(player);
                addItem(itemToBuy);
            }
            else{
                Debug.Log("No space in ability inventory");
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
