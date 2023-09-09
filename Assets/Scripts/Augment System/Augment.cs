using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Augment : ScriptableObject
{
    [SerializeField]
    private string aName = "New Augment";
    [SerializeField]
    private Sprite aSprite;
    [SerializeField]
    private AudioClip aSound; // not sure if each one will have unique sound
    [SerializeField]
    private float activeTime = 1f; // not sure if they will all be timed or not
    [SerializeField]
    private bool augmentEnabled = false; // prevent stacking or have some sort of mechanism for stacking

    public void enableAugment()
    {
        if (augmentEnabled)
        {
            stackAugments();
        }
        else
        {
            firstActivation();
            augmentEnabled = true;
        }
    }

    public virtual void firstActivation()
    {
        // initial buff
    }

    public virtual void stackAugments()
    {
        // return if you don't want stacking
        // or implement custom mechanism
    }

    public virtual float applyAugmentDamageTaken(float damageTaken)
    {
        // used in AugmentManager to actually apply
        return 0;
    }

    public virtual float applyAugmentDamageDealt(float damageDealt)
    {
        // used in AugmentManager to actually apply
        return 0;
    }

    public Augment GetAugment()
    {
        return this;
    }

}
