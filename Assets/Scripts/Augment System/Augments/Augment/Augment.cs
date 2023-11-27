using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AugmentType { STAT, TICK, ONHIT }

public class Augment : ScriptableObject {

    public string aName = "New Augment";
    public string aDescription = "New Description";
    public Sprite aSprite;
    public AudioClip aSound; // not sure if each one will have unique sound
    public bool expires = false;
    public float expirationTime = 0f;
    public AugmentType AugmentType;
    public List<StatModifier> statModifiers;
    public virtual IEnumerator _Enable(GameObject parent, GameObject vfxObject) => null;
    public virtual void _Disable(GameObject parent, GameObject vfxObject) {}
    public virtual float _ApplyAugmentDamageTaken(float damageTaken, Entity source, Entity target) => 0;
    public virtual float _ApplyAugmentDamageDealt(float damageDealt, Entity source, Entity target, HashSet<AbilityTag> tags) => 0;

}