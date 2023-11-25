using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu]
public class BuffDamage : Item
{
    [SerializeField]
    private float extraDamage = 10;

    public override float applyItemDamageDealt(float damageDealt, Entity current, Entity target)
    {
        return extraDamage;
    }
}
