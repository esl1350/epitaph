using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GraveQuakeAbility : Ability
{
    public float damage;
    private Animator animator;
    public CircleSlashHitbox hitbox;
    private CircleSlashHitbox hitboxInstance;
    private Camera mainCamera;
    public float maxChargeTime;
    public float minChargeTime;
    private float currentChargeTime;

    private float chargedDamage;
    public void Activate(GameObject parent) {
        hitboxInstance = Instantiate(hitbox, parent.transform);
        hitboxInstance.parent = parent;
        hitboxInstance.damage = damage;
        hitboxInstance.GetComponent<CircleCollider2D>().enabled = false;
        animator = hitboxInstance.gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // public void Deactivate(GameObject parent) {    
    //     Destroy(hitboxInstance.gameObject);
    // }

    public void Explode() {
        if (animator != null) {
            animator.SetTrigger("Explode");
        }
        if (hitboxInstance != null) {
            //hitboxInstance.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    public override void AbilityCooldownHandler(GameObject parent) {
        switch (state) 
        {
            case AbilityState.ready:
                if (abilityPressed) {
                    Activate(parent);
                    currentChargeTime = maxChargeTime;
                    fillAmount = 1;
                    state = AbilityState.charge;
                    chargedDamage = damage;
                    // abilityPressed = false;
                }
            break;
            case AbilityState.charge:
                if (currentChargeTime > 0) {
                    currentChargeTime -= Time.deltaTime;
                    chargedDamage += .02f;
                    hitboxInstance.damage = chargedDamage;
                    // fillAmount -= 1/cooldownTime * Time.deltaTime;
                } else {
                    state = AbilityState.active;
                }
                if (!abilityPressed && currentChargeTime < minChargeTime) {
                    state = AbilityState.active;
                }
            break;
            case AbilityState.active:
                Explode();
                currentCooldownTime = cooldownTime;
                state = AbilityState.cooldown;
            break;
            case AbilityState.cooldown:
                if (currentCooldownTime > 0) {
                    currentCooldownTime -= Time.deltaTime;
                    fillAmount -= 1/cooldownTime * Time.deltaTime;
                } else {
                    state = AbilityState.ready;
                    fillAmount = 1;
                }
            break;
        }
    }

}
