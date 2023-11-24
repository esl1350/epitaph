using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Berserk Augment", menuName = "Augments/Berserk")]
public class BerserkAugment : Augment
{

    [SerializeField]
    private float damageTakenModifier;

    [SerializeField]
    private float damageDealtModifier;


    public override float _ApplyAugmentDamageDealt(float damageDealt, Entity current, Entity target, HashSet<AbilityTag> tags)
    {
        return damageDealt * (damageDealtModifier - 1f);
    }

    public override float _ApplyAugmentDamageTaken(float damageDealt, Entity current, Entity target)
    {
        return damageDealt * (damageTakenModifier - 1f);
    }

}
