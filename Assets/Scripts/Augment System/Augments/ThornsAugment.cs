using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsAugment : Augment
{
    [SerializeField]
    private float damageFactor = 0.5f; // assuming that you deal damage by a factor of what you take

    private float damageTaken;


    public override void stackAugments()
    {
        return;
    }

    public override void firstActivation()
    {
        Debug.Log("LifeSteal activated.");
    }

    public override float applyAugmentDamageDealt(float damageDealt)
    {
        return damageTaken;
    }

    public override float applyAugmentDamageTaken(float damageTaken)
    {
        this.damageTaken = damageTaken;
        return 0;
    }

}
