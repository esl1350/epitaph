using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    protected int cost; // cost of the item

    [SerializeField]
    protected string itemName; // name of the item

    [SerializeField]
    protected Sprite abilitySprite; // sprite of the item

    [SerializeField]
    protected AbilityWrapper ability = null; // sprite of the item

    [SerializeField]
    protected bool enabled;

    public int getCost()
    {
        return this.cost;
    }

    public string getName()
    {
        return this.itemName;
    }

    public Sprite getSprite()
    {
        return this.abilitySprite;
    }


    public bool isAbility()
    {
        //Abilities should cost over 120 coins, other items should be under this amount
        if(this.ability != null){
            return true;
        }
        else{
            return false;
        }
    }

    public void disable()
    {
        enabled = false;
    }

    public virtual void activate(Player player)
    {
        Debug.Log("Activating: " + itemName);
        if (!enabled)
        {
            firstActivation(player);
        }
        enabled = true;
    }

    public virtual void deactivate(Player player)
    {
        enabled = false;
        disableStatic(player);
    }

    public virtual void firstActivation(Player player)
    {
        // can use for static items such as adding health
    }

    public virtual void disableStatic(Player player)
    {
        // for temporary static buffs
    }

    public virtual float applyItemDamageTaken(float damageTaken, Entity current, Entity target)
    {
        return 0;
    }

    public virtual float applyItemDamageDealt(float damageDealt, Entity current, Entity target)
    {
        return 0;
    }

}
