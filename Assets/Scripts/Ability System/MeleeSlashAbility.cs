using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeSlashAbility : Ability
{

    public float damage;
    public float knockbackForce;
    public float knockbackDuration;
    private Camera mainCamera;
    public MeleeSlashHitbox hitbox;
    private MeleeSlashHitbox hitboxInstance;
    private Vector3 mousePos;

    public override void Init() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //animation = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation>();
    }

    public override void Activate(GameObject parent) {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mouseDirection = mousePos - parent.transform.position;
        Vector3 offset = new Vector3(mouseDirection.x, mouseDirection.y, 0).normalized;
        
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * offset;
        Quaternion rotationToTarget = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        // offset of hitbox from player is hardcorded
        hitboxInstance = Instantiate(hitbox, parent.transform.position + (offset * 1.5f), rotationToTarget, parent.transform);
        hitboxInstance.parent = parent;
        hitboxInstance.damage = damage;
        hitboxInstance.knockbackForce = knockbackForce;
        hitboxInstance.knockbackDuration = knockbackDuration;
        // Debug.Log("Melee Slash");
        //_animator.Play();
    }

    public override void Deactivate(GameObject parent) {
        
        Destroy(hitboxInstance.gameObject);
        // Debug.Log("Melee Slash Done");
    }

    public override void AbilityCooldownHandler(GameObject parent) {
        switch (state) 
        {
            case AbilityState.ready:
                if (abilityPressed) {
                    Activate(parent);
                    state = AbilityState.active;
                    currentActiveTime = activeTime;
                    fillAmount = 1;
                }
            break;
            case AbilityState.active:
                if (currentActiveTime > 0) {
                    currentActiveTime -= Time.deltaTime;
                } else {
                    state = AbilityState.cooldown;
                    currentCooldownTime = cooldownTime;
                    Deactivate(parent);
                }
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
