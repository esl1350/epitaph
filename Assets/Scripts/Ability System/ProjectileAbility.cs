using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileAbility : Ability
{
    protected Camera mainCamera;
    protected Vector2 mousePos;
    protected bool firing;
    public Projectile projectile;

    //public GameObject bulletSpawner;
    //public Transform bulletTransform;
    
    protected bool canFire;
    protected float timer;
    public float fireRate;

    public override void Activate(GameObject parent)
    {
        firing = true;
    }

    public override void Deactivate(GameObject parent) 
    {
        firing = false;
    }

    public override void Init() {
        //bulletTransform = bulletSpawner.transform;
        canFire = true;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public override void AbilityBehavior(GameObject parent) {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Vector3 rotation = mousePos - parent.transform.GetChild(0).transform.position;
        // float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        // parent.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, rotZ);
         
        if (!canFire) {
            timer += Time.deltaTime;
            if (timer > fireRate) {
                canFire = true;
                timer = 0;
            }
        }

        if (firing && canFire) {
            canFire = false;
            Projectile projectileCopy = Instantiate(projectile, parent.transform.GetChild(0).transform.position, Quaternion.identity);
            projectileCopy.parent = parent;
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