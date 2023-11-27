using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float moveSpeedMultiplier;

    private SpeedIncrease modifier;
    
    public override void Init() {
        modifier = new SpeedIncrease(moveSpeedMultiplier);
    }

    public override void Activate(GameObject parent)
    {
        parent.GetComponent<Animator>().SetTrigger("dash startup");
        parent.GetComponent<Animator>().SetBool("is dashing", true);
        ModifiableStat speed = parent.GetComponent<Player>().EntityStats.GetStat(StatEnum.WALKSPEED);
        speed.AddModifier(modifier);
        parent.GetComponent<PlayerController>().CanChangeDirection = false;
        float dashDist = this.activeTime * speed.GetStatValue();
        Vector2 dashDir = parent.GetComponent<PlayerController>().LastMovementInput;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDash, parent.transform.position);
        Debug.Log(dashDir);
        if (!IsInsideTerrain(parent, dashDist, dashDir)) {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Terrain"), LayerMask.NameToLayer("Player"), true);
        }
    }

    public override void Deactivate(GameObject parent)
    {
        parent.GetComponent<Animator>().SetBool("is dashing", false);
        parent.GetComponent<Animator>().ResetTrigger("dash startup");
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Terrain"), LayerMask.NameToLayer("Player"), false);
        ModifiableStat speed = parent.GetComponent<Player>().EntityStats.GetStat(StatEnum.WALKSPEED);
        speed.RemoveModifier(modifier);
        parent.GetComponent<PlayerController>().CanChangeDirection = true;
    }
    private bool IsInsideTerrain(GameObject parent, float dist, Vector2 dir) {
        LayerMask mask = LayerMask.GetMask("Terrain");
        Vector2 destination = new Vector2(parent.transform.position.x, parent.transform.position.y) + dir*dist;
        Collider2D[] hits = Physics2D.OverlapPointAll(destination, mask);
        if (hits.Length > 0) {
            foreach (Collider2D hit in hits) {
                Debug.Log(hit.gameObject.name);
            }
            return true;
        }
        return false;
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