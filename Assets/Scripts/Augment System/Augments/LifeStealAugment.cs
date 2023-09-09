using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LifeStealAugment : Augment
{
    [SerializeField]
    private float stealFactor = 0.5f; // assuming that you gain health by a factor of the damage you deal

    private float damageDealt;

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
        this.damageDealt = damageDealt;
        return 0;
    }

    // note that this is negative because we want to heal
    public override float applyAugmentDamageTaken(float damageTaken)
    {
        return -stealFactor * damageDealt;
    }
}
