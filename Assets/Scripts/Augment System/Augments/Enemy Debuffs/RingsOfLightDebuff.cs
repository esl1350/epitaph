using UnityEngine;

[CreateAssetMenu]
public class RingsOfLightDebuff : Augment
{
    [SerializeField]
    private float damage = 5f;

    public override float _ApplyAugmentDamageTaken(float damageDealt, Entity current, Entity target)
    {
        Debug.Log("Samsara damage spent");

        AugmentManager targetAugmentManager = current.GetComponent<AugmentManager>();
        if (!targetAugmentManager) return 0;

        targetAugmentManager.MarkRemoveAugment(this);
        return damage;
    }
}
