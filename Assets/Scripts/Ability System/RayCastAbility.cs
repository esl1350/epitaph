using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RayCastAbility : Ability
{

    public override void Activate(GameObject parent)
    {
        parent.GetComponent<RayCastTrigger>().Fire();
    }

    public override void Deactivate(GameObject parent) 
    {
        parent.GetComponent<RayCastTrigger>().Stop();
    }

        public override void AbilityHandler(GameObject parent) {
        //Debug.Log("reached?ray");
            switch (state) 
            {
                case AbilityState.ready:
                    if (abilityPressed) {
                        Activate(parent);
                        state = AbilityState.active;
                        currentActiveTime = activeTime;
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
                    } else {
                        state = AbilityState.ready;
                    }
                break;
            }
    }
}
