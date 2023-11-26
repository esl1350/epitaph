using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu]
public class BuffSpeed : Item
{
    [SerializeField]
    private float extraSpeed = 1;

    public override void firstActivation(Player player)
    {
        Debug.Log("Increasing speed by: " + extraSpeed);
        player.Walkspeed.currentBaseValue += extraSpeed;
    }

    public override void disableStatic(Player player)
    {
        player.Walkspeed.currentBaseValue -= extraSpeed;
    }
}
