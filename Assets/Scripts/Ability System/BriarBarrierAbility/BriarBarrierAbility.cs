using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BriarBarrierAbility : Ability
{

    protected Camera mainCamera;
    protected Vector2 mousePos;

    [SerializeField]
    private BriarBarrierController bbController;

    [SerializeField]
    private float radius;

    protected bool firing;

    //public GameObject bulletSpawner;
    //public Transform bulletTransform;

    public override void Activate(GameObject parent)
    {
        Debug.Log("Dead Spark Activated");
        firing = true;
        SetOnCooldown();
    }

    public override void Deactivate(GameObject parent) 
    {
    }

    public override void Init() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public override void AbilityBehavior(GameObject parent) {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (firing) {
            Instantiate(bbController, parent.transform);
            firing = false;
        }
    }

    public override void AbilityCooldownHandler(GameObject parent) {
        switch (state) 
        {
            case AbilityState.ready:
                if (abilityPressed) Activate(parent);
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

    private void SetOnCooldown() {
        state = AbilityState.cooldown;
        currentCooldownTime = cooldownTime;
        fillAmount = 1;
        abilityPressed = false;
    }   

}