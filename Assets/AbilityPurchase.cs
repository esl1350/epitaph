using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu]
public class AbilityPurchase : Item
{
    [SerializeField]
    public AbilityWrapper ability;

    public Sprite abilitySprite;

    public override void firstActivation(Player player)
    {
        AbilityInventoryManager abilityInventory = player.GetComponent<AbilityInventoryManager>();
        Debug.Log("Adding ability");
        bool didHappen = abilityInventory.Add(ability);
    }
}
