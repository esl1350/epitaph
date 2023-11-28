using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu]
public class BuffDamage : Item
{
    [SerializeField]
    private float extraDamage = 10;

    private Damage modifier;

    public Sprite abilitySprite;

    public override void firstActivation(Player player)
    {
        Debug.Log("Increasing damage by: " + extraDamage);
        modifier = new Damage(extraDamage);
        ModifiableStat damage = player.EntityStats.GetStat(StatEnum.ATTACK);
        damage.AddModifier(modifier);
    }
}
