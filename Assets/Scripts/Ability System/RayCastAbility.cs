using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

[CreateAssetMenu]
public class RayCastAbility : Ability
{
    private HashSet<AbilityTag> tags = new HashSet<AbilityTag> { AbilityTag.FIRE };
    private LineRenderer lineRenderer;
    private LaserParticleMan laserParticleMan;
    private MandalaManager mandalaMan;
    private Camera mainCamera;
    private Vector3 mousePos;
    private bool firing;

    public float rayWidth;
    public float range;
    public float damage;
    public float tickRate;

    private bool canTick;
    private float timer;

    private Vector3 _prevPos;

    public override void Activate(GameObject parent)
    {
        mandalaMan.StartCoroutine(ActivateRoutine());
    }

    IEnumerator ActivateRoutine() {
        mandalaMan.Activate();
        yield return new WaitForSeconds(0.5f);
        firing = true;
    }

    public override void Deactivate(GameObject parent) 
    {
        firing = false;
        mandalaMan.Deactivate();

        // Added so we can deactivate laser on demand too
        lineRenderer.enabled = false;
        EnableParticleMan(false);
    }

    public override void Init()
    {
        lineRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        
        laserParticleMan = lineRenderer.gameObject.GetComponentInChildren<LaserParticleMan>(includeInactive: true);
        mandalaMan = lineRenderer.gameObject.GetComponentInChildren<MandalaManager>(includeInactive: true);
        
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canTick = true;
        firing = false;
        Debug.Log("Init Mandala");
    } 

    //Tells the shader how long the laser is so the shader scales correctly.
    private void SetLineRMatLen(float len) {
        lineRenderer.material.SetFloat("_Length", len);
    }

    private void EnableParticleMan(bool b) {
        laserParticleMan.gameObject.SetActive(b);
    }

    public override void AbilityBehavior(GameObject parent) {
        if (firing) {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector3 pos1 = mandalaMan.GetMandalaCenter(); //laser origin should be the mandala's center
            Vector3 pos2 = mousePos + new Vector3(0,0,10);

            _prevPos = pos2;

            lineRenderer.SetPosition(0, pos1);
            lineRenderer.SetPosition(1, pos2);
            lineRenderer.enabled = true;
            EnableParticleMan(true);
            laserParticleMan.UpdateParticles(pos1, pos2);

            float len = (pos2-pos1).magnitude;
            SetLineRMatLen(len);

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
                    parent.GetComponent<Entity>().DealDamage(hit.transform.gameObject.GetComponent<Entity>(), damage, tags);
                    canTick = false;
                }

            }

        } else {
            lineRenderer.enabled = false;
            EnableParticleMan(false);
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
