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
        GameObject.FindWithTag("CMCam").GetComponent<CameraShake>().Shake(1.0f, 3.0f);

        AudioManager.instance.PlayOneShot(FMODEvents.instance.quakeFirst, parent.transform.position);
    }

    // public void Deactivate(GameObject parent) {    
    //     Destroy(hitboxInstance.gameObject);
    // }

    public void Explode(GameObject parent) {
        GameObject.FindWithTag("CMCam").GetComponent<CameraShake>().Shake(2.0f, 0.5f);
        if (animator != null) {
            animator.SetTrigger("Explode");
            AudioManager.instance.PlayOneShot(FMODEvents.instance.quakeFinish, parent.transform.position);
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
                    chargedDamage += .03f;
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
                Explode(parent);
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
