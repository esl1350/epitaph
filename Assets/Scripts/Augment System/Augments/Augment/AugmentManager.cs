using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentManager : MonoBehaviour
{

    [SerializeField] private bool DEBUG = false;

    [SerializeField]
    private Entity current;

    private Entity target;

    private float damageTaken = 0;

    private float damageDealt = 0;

    // active augments should be handled in the backend, no need to expose
    
    // Trying something new here - not deleting old logic for now
    private readonly HashSet<AugmentInstance> augments = new();
    private readonly HashSet<Augment> augmentsToRemove = new();
    public void setCurrent(Entity current)
    {
        this.current = current;
    }

    #region Run Event Triggers

    public void DebugPrintAugments() {
        Debug.Log("[Augment Manager] Augments");
        string s = "";
        foreach (AugmentInstance a in augments)
            s += a;
        if (s != "") Debug.Log(s);
        else Debug.Log("No Augments");
    }

    /*
    * Unused
    * This should be called right before player gets control on a run room i guess
    */
    public void OnRunStart() {
        ActivateAllAugments();
    }

    /*
    * Also unused
    * Called right after player loses control on a run room
    */
    public void OnRunEnd() {
        DeactivateAllAugments();
    }

    #endregion

    #region Listener
    /*
    * do not panic!
    * This is the part that makes it so we know what to add as an augment or not
    * Will attach to the passives bar and update the list of passives whenever there is a change 
    */

    public void PassiveSlotCallback(Slot<AbilityWrapper> slot, AbilityWrapper old) {
        if (slot.Item) AddAugment(slot.Item.PassiveAbility);
        if (old) RemoveAugment(old.PassiveAbility);
    }
    
    public void SetupPassiveBarListener(SlotHolder<AbilityWrapper> passives) {
        for (int i = 0; i < passives.Length; i++)
            passives[i].callback = PassiveSlotCallback;
    }
    #endregion

    #region QOL utils

    private void ActivateAllAugments() {
        foreach (AugmentInstance augment in augments)
            augment.Enable(gameObject, this);
    }

    private void DeactivateAllAugments() {
        foreach (AugmentInstance augment in augments)
            augment.Disable(gameObject, this);
    }

    #endregion

    #region On-Hit Augments
    /*
    *   We have a bit of an issue here
    *   Apparently static and listener augments should be able to alter damage across the board
    *   There's probably something we can do here to make that happen
    *   Like half of the proposed passives need this to happen :(
    */

    // called whenever the player takes damage
    public void updateDamageTaken(float damage)
    {
        this.damageTaken = damage;
        takeAugmentedDamage();
    }

    // called whenever the player deals damage
    public void updateDamageDealt(Entity target, float damage, HashSet<AbilityTag> tags)
    {
        this.target = target;
        this.damageDealt = damage;
        dealAugmentedDamage(tags);
    }

    // this applies the augment AFTER we already took damage
    public void takeAugmentedDamage()
    {
        foreach (AugmentInstance augment in augments)
            current.TakeDamageAugmented(augment.ApplyAugmentDamageTaken(damageTaken, current, target));

        ResolveRemovedAugments();
    }

    // applies the augment AFTER we already deal damage
    public void dealAugmentedDamage(HashSet<AbilityTag> tags)
    {
        foreach (AugmentInstance augment in augments)
            current.DealDamageAugmented(target, augment.ApplyAugmentDamageDealt(damageDealt, current, target, tags));

        ResolveRemovedAugments();
    }

    #endregion

    #region General Utils
    public void AddAugment(Augment augment) {
        if (DEBUG) Debug.Log("Adding augment");
        if (DEBUG) Debug.Log(augment.GetType());

        AugmentInstance newInstance = new()
        {
            instance = augment
        };

        if (augments.Contains(newInstance) == true) {
            // throw new Exception("[AugmentManager.cs] Tried to add an augment that is already in the list of current augments");
            Debug.LogWarning("[AugmentManager.cs] Tried to add an augment that is already in the list of current augments");
            return;
        }

        augments.Add(newInstance);
    }

    private void ResolveRemovedAugments() {
        foreach (Augment augment in augmentsToRemove)
            RemoveAugment(augment);
        augmentsToRemove.Clear();
    }
    public void MarkRemoveAugment(AugmentInstance augment) {
        if (augmentsToRemove.Contains(augment.instance) == false)
            augmentsToRemove.Add(augment.instance);
    }

    public void MarkRemoveAugment(Augment augment) {
        if (augmentsToRemove.Contains(augment) == false)
            augmentsToRemove.Add(augment);
    }

    public void RemoveAugment(Augment augment) {
        if (DEBUG) Debug.Log("Removing augment");
        if (DEBUG) Debug.Log(augment.GetType());

        foreach (AugmentInstance _augmentInstance in augments) {
            if (_augmentInstance.Equals(augment)) {
                if (_augmentInstance.AugmentState == AugmentState.ACTIVE) _augmentInstance.Disable(gameObject);
                augments.Remove(_augmentInstance);
                return;
            }
        }

        Debug.LogWarning("[AugmentManager.cs] Tried to remove an augment that is not in the list of current augments");
        return;

    }

    public void ClearAugments() {
        if (DEBUG) Debug.Log("[Augment Manager] Clearing all augments");
        augments.Clear();
    }

    #endregion

}
