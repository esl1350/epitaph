using UnityEngine;

[CreateAssetMenu]
class RingsOfLightAbility : ProjectileAbility {

    public new RingOfLightProjectile projectile;

    [SerializeField]
    private float range;

    public override void Init()
    {
        base.Init();
    }

    public override void Activate(GameObject parent)
    {
        base.Activate(parent);
        SetOnCooldown();
        Debug.Log("Samsara Activated");
    }

    public override void Deactivate(GameObject parent)
    {
        base.Deactivate(parent);
    }

    public override void AbilityCooldownHandler(GameObject parent)
    {
        switch(state) {
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
            default:
            break;
        }
    }

    public override void AbilityBehavior(GameObject parent)
    {
        Debug.Log("CanFire: " + canFire);
        Debug.Log("firing: " + firing);

        if (!canFire) {
            timer += Time.deltaTime;
            if (timer > fireRate) {
                canFire = true;
                timer = 0;
            }
        }

        if (!firing || !canFire) return;

        canFire = false;
        Vector2 pos = parent.transform.position;
        ThrowDiscs(pos, parent);
        Deactivate(parent);
        
    }

    private void SetOnCooldown() {
        currentCooldownTime = cooldownTime;
        state = AbilityState.cooldown;
        fillAmount = 1;
        abilityPressed = false;
    }

    private void ThrowDiscs(Vector2 pos, GameObject parent) {

        for (int i = 0; i < 8; i++) {
            Vector3 direction = Quaternion.AngleAxis(i/ 8f * 360, Vector3.forward) * Vector3.up;
            RingOfLightProjectile disc = Instantiate(projectile, pos, Quaternion.identity);
            disc.parent = parent;
            disc.direction = direction;
        }
    }

}