using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float moveSpeedMultiplier;

    public override void Activate(GameObject parent)
    {
        Debug.Log("dash start");
        parent.GetComponent<PlayerController>().moveSpeed *= moveSpeedMultiplier;
        parent.GetComponent<PlayerController>().canChangeDirection = false;
    }

    public override void Deactivate(GameObject parent)
    {
        Debug.Log("dash end");
        parent.GetComponent<PlayerController>().moveSpeed /= moveSpeedMultiplier;
        parent.GetComponent<PlayerController>().canChangeDirection = true;
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