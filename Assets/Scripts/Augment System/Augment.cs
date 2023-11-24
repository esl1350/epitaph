// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Augment : ScriptableObject
// {
//     [SerializeField]
//     private string aName = "New Augment";
//     [SerializeField] public string aDescription = "New Description";

//     [SerializeField]
//     private Sprite aSprite;
//     [SerializeField]
//     private AudioClip aSound; // not sure if each one will have unique sound
//     [SerializeField]
//     private float activeTime = 1f; // not sure if they will all be timed or not
//     [SerializeField]
//     private bool augmentEnabled = true; 

//     public void enableAugment()
//     {
//         if (!augmentEnabled)
//         {
//             firstActivation();
//         }
//         augmentEnabled = true;
//     }

//     public void disableAugment()
//     {
//         augmentEnabled = false;
//     }

//     public virtual void firstActivation()
//     {
//         // initial buff
//     }


//     public Augment GetAugment()
//     {
//         return this;
//     }

//     public virtual float applyAugmentDamageTaken(float damageTaken, Entity current, Entity target)
//     {
//         // used in AugmentManager to actually apply
//         return 0;
//     }

//     public virtual float applyAugmentDamageDealt(float damageDealt, Entity current, Entity target, HashSet<AbilityTag> tags)
//     {
//         // used in AugmentManager to actually apply
//         return 0;
//     }

//     // For VFX
//     public virtual void OnApply(Entity parent) {
//         return;
//     }

//     public virtual void OnExpire(Entity parent) {
//         return;
//     }
// }
