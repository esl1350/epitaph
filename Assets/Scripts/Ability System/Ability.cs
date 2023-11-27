using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// What other damage types do we want to have?
public enum AbilityTag { PHYSICAL, FIRE, AOE };
public class Ability : ScriptableObject
{
    public HashSet<AbilityTag> tags;
    public string aName = "New Ability";
    public string aDescription = "New Description";
    public Sprite aSprite;
    public AudioClip aSound;
    public float cooldownTime = 1f;
    public float activeTime = 1f;
    protected AbilityState state;
    protected bool abilityPressed;
    protected float currentActiveTime;
    protected float currentCooldownTime;
    public float fillAmount;
    // [SerializeField] protected AnimationClip[] _animationClips;

    // protected Animation animation;
    public virtual void Activate(GameObject parent) {}
    public virtual void Deactivate(GameObject parent) {}
    // Handles ability cooldown system
    public virtual void AbilityCooldownHandler(GameObject parent) {}
    public virtual void Init() {}
    // Handles what an ability does
    public virtual void AbilityBehavior(GameObject parent) {}

    public void SetAbilityPressed(bool pressed) {
        abilityPressed = pressed;
    }

    public void SetState(AbilityState state) {
        this.state = state;
    }

    // Resets skill (useful when swapping skills)
    public void Reset(GameObject parent) {
        currentCooldownTime = 0f;
        currentActiveTime = 0f;
        if (state == AbilityState.active)
            Deactivate(parent);
        state = AbilityState.ready;
        fillAmount = 1f;
    }

    public Ability GetAbility() {
        return this;
    }
}

public enum AbilityState {
    ready,
    reactive,
    charge,
    active,
    cooldown
}


