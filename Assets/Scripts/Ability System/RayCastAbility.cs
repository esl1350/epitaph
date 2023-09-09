using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

[CreateAssetMenu]
public class RayCastAbility : Ability
{
    private LineRenderer lineRenderer;
    private Camera mainCamera;
    private Vector3 mousePos;
    private bool firing;

    public float rayWidth;
    public float range;
    public float damage;
    public float tickRate;

    private bool canTick;
    private float timer;

    public override void Activate(GameObject parent)
    {
        firing = true;
    }

    public override void Deactivate(GameObject parent) 
    {
        firing = false;
    }

    public override void Init()
    {
        lineRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canTick = true;   
    } 

    public override void AbilityBehavior(GameObject parent) {
        if (firing) {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, parent.transform.GetChild(0).transform.position);
            lineRenderer.SetPosition(1, mousePos + new Vector3(0,0,10));
            lineRenderer.enabled = true;

            LayerMask mask = LayerMask.GetMask("Enemy");
            RaycastHit2D[] hits = Physics2D.CircleCastAll(parent.transform.GetChild(0).transform.position, rayWidth/2, mousePos - parent.transform.GetChild(0).transform.position, range, mask, -5, 5);
            foreach (RaycastHit2D hit in hits) {
                if (!canTick) {
                    timer += Time.deltaTime;
                    if (timer > tickRate) {
                        canTick = true;
                        timer = 0;
                    }
                }

                if (canTick) {
                    parent.GetComponent<Entity>().DealDamage(hit.transform.gameObject.GetComponent<Entity>(), damage);
                    Debug.Log(hit);
                    canTick = false;
                }

            }

        } else {
            lineRenderer.enabled = false;
        }
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
