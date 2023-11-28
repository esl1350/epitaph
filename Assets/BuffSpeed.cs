using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu]
public class BuffSpeed : Item
{
    [SerializeField]
    private float extraSpeed = 1.1f;
    private SpeedIncrease modifier;

    public Sprite abilitySprite;
    public override void firstActivation(Player player)
    {
        Debug.Log("Increasing speed by this times: " + extraSpeed);
        modifier = new SpeedIncrease(extraSpeed);
        ModifiableStat speed = player.EntityStats.GetStat(StatEnum.WALKSPEED);
        speed.AddModifier(modifier);
    }
}
