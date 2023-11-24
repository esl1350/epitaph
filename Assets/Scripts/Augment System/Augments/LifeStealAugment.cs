using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Life Steal Augment", menuName = "Augments/Life Steal")]
public class LifeStealAugment : Augment
{
    [SerializeField]
    private float stealFactor = 0.5f; // assuming that you gain health by a factor of the damage you deal

    public override float _ApplyAugmentDamageDealt(float damageDealt, Entity current, Entity target, HashSet<AbilityTag> tags)
    {
        if (current.gameObject.TryGetComponent(out StatusEffectManager sem)) {
            sem.AddModifiers(new List<StatModifier>{ new Heal(damageDealt * stealFactor)});
        }
        return 0;
    }

}
