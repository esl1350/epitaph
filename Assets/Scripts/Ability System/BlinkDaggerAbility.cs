using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BlinkDaggerAbility : ProjectileAbility
{
    //private Camera mainCam;
    //private Vector3 mousePos;
    //private bool firing;
    // public GameObject dagger;
    //private bool canFire;
    public float delay;
    private bool daggerThrown;
    private Projectile thrownDagger;
    
    public bool teleported;

    // public override void Activate(GameObject parent)
    // {
    //     firing = true;
    // }

    // public override void Deactivate(GameObject parent) 
    // {
    //     firing = false;
    // }

    public override void Init() {
        base.Init();
        firing = false;
        teleported = false;
        daggerThrown = false;
    }

    public override void AbilityBehavior(GameObject parent) {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (!canFire) {
            timer += Time.deltaTime;
            if (timer > delay) {
                canFire = true;
                timer = 0;
            }
        }

        if (firing && !daggerThrown && canFire) {
            Vector2 pos = parent.transform.GetChild(0).GetChild(0).transform.position;
            ThrowDagger(pos, parent);
        } else if (firing && daggerThrown && canFire) {
            GoToDagger(parent);
        }
    }

    public override void AbilityCooldownHandler(GameObject parent) {
        switch (state) 
        {
            case AbilityState.ready:
                if (abilityPressed) {
                    Activate(parent);
                    state = AbilityState.reactive;
                    fillAmount = 1;
                    abilityPressed = false;
                }
            break;
            case AbilityState.reactive:
                if (abilityPressed) {
                    Activate(parent);
                    state = AbilityState.active;
                }
            break;
            case AbilityState.active:
                if (teleported == true) {
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

    #region DaggerMethods
    private void ThrowDagger(Vector2 pos, GameObject parent) {
        daggerThrown = true;
        canFire = false;
        teleported = false;
        thrownDagger = Instantiate(projectile, pos, Quaternion.identity);
        thrownDagger.parent = parent;
        firing = false;
    }

    private void GoToDagger(GameObject parent) {
        daggerThrown = false;
        canFire = false;
        teleported = true;
        parent.transform.position = thrownDagger.transform.position;
        Destroy(thrownDagger.gameObject);
        firing = false;
    }
    #endregion
}
