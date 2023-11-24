using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class TalismanAbility : Ability
{
    [SerializeField] private GameObject talismanTemplate;
    [SerializeField] private float objectTime; // how long the player has to place the second sigil
    private TalismanSigil talisman;

    protected bool reactivate;
    protected bool secondSigilPlaced;
    private float objectTimeActive;
    public override void Init() {
        base.Init();
        secondSigilPlaced = false;
        reactivate = false;
    }

    public override void Activate(GameObject parent) {
        if (!reactivate) {
            talisman = Instantiate(talismanTemplate, parent.transform.GetChild(0).GetChild(0).transform.position, Quaternion.identity).GetComponent<TalismanSigil>();
            reactivate = true;
            objectTimeActive = 0f;

        } else if (reactivate && !secondSigilPlaced) {
            talisman.AddNewSigil(parent.transform.position);
            secondSigilPlaced = true;
        }
    }

    public override void AbilityBehavior(GameObject parent) {

    }
    public override void AbilityCooldownHandler(GameObject parent) {
        switch (state) 
        {
            case AbilityState.ready:
                secondSigilPlaced = false;
                reactivate = false;            
                if (abilityPressed) {
                    Activate(parent);
                    reactivate = false;
                    state = AbilityState.reactive;
                    fillAmount = 1;
                    abilityPressed = false;
                }
            break;
            case AbilityState.reactive:
                objectTimeActive += Time.deltaTime;
                reactivate = true;
                secondSigilPlaced = false;
                if (objectTimeActive > objectTime) {
                    Destroy(talisman.gameObject);
                    state = AbilityState.active;
                }
                else if (abilityPressed) {
                    Activate(parent);
                    state = AbilityState.active;
                }
            break;
            case AbilityState.active:
                if (secondSigilPlaced || talisman == null) {
                    currentCooldownTime = cooldownTime;
                    state = AbilityState.cooldown;
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
