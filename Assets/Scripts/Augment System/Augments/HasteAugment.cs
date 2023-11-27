using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Haste Augment", menuName = "Augments/Haste")]
public class HasteAugment : Augment
{

    [SerializeField]
    private StatusEffect hasteEffect;
    public override float _ApplyAugmentDamageTaken(float damageDealt, Entity current, Entity target)
    {
        if (current.gameObject.TryGetComponent(out StatusEffectManager sem)) {
            sem.ApplyEffect(hasteEffect);
        }
        return 0;
    }

}
