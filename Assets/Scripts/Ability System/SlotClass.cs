using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotClass
{
    [SerializeField] private AbilityWrapper ability;
    
    public SlotClass() {
        ability = null;
    }

    public SlotClass (AbilityWrapper ability) {
        this.ability = ability;
    }

    public SlotClass (SlotClass slot) {
        this.ability = slot.GetAbility();
    }

    public AbilityWrapper GetAbility() {
        return ability;
    }
    
    public void AddAbility(AbilityWrapper ability) {
        this.ability = ability;
    }

    public void Clear() {
        this.ability = null;
    }

    public bool isClear() {
        return this.ability == null;
    }
}
