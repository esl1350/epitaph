using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    // need a reference to this to adjust damage dealt and taken
    [SerializeField]
    private AugmentManager _augmentManager;

    // relaying data to augment manager
    public override void TakeDamage(float amount)
    {
        Health.TakeDamage(amount);
        _augmentManager.updateDamageTaken(amount);
    }

    // relaying data to augment manager
    public override void DealDamage(Entity target, float dmgAmt)
    {
        target.TakeDamage(dmgAmt);
        _augmentManager.updateDamageDealt(target, dmgAmt);
    }

    // handle augmented damage taken after initial
    public void TakeDamageAugmented(float amount)
    {
        Health.TakeDamage(amount);
    }

    // handle augmented damage dealt after initial
    public void DealDamageAugmented(Entity target, float dmgAmt)
    {
        target.TakeDamage(dmgAmt);
    }
}
