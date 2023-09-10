using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class AttackBuffAbility : Ability
{
    public float damageMultiplier;

    public override void Activate(GameObject parent)
    {
        Debug.Log("attack buff start");
        Debug.Log("Original attack: " + parent.GetComponent<Player>().Attack);
        parent.GetComponent<Player>().Attack *= damageMultiplier;
        Debug.Log("Damage Multiplier: " + damageMultiplier); 
        Debug.Log("Buffed damage: " + parent.GetComponent<Player>().Attack);
    }

    public override void Deactivate(GameObject parent)
    {
        Debug.Log("attack buff end");
        float buffedAttack = parent.GetComponent<Player>().Attack;
        float originalAttack = buffedAttack / damageMultiplier;
        parent.GetComponent<Player>().Attack = originalAttack;
        Debug.Log("original attack: " + originalAttack);
        Debug.Log("parent.GetComponent<Player>().Attack: " + parent.GetComponent<Player>().Attack);
        
    }

    public override void AbilityCooldownHandler(GameObject parent)
    {
        switch (state)
        {
            case AbilityState.ready:
                if (abilityPressed)
                {
                    Activate(parent);
                    state = AbilityState.active;
                    currentActiveTime = activeTime;
                    fillAmount = 1;
                }
                break;
            case AbilityState.active:
                if (currentActiveTime > 0)
                {
                    currentActiveTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.cooldown;
                    currentCooldownTime = cooldownTime;
                    Deactivate(parent);
                }
                break;
            case AbilityState.cooldown:
                if (currentCooldownTime > 0)
                {
                    currentCooldownTime -= Time.deltaTime;
                    fillAmount -= 1 / cooldownTime * Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                    fillAmount = 1;

                }
                break;
        }
    }

}