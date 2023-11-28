using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DeadSparkAbility : Ability
{

    protected Camera mainCamera;
    protected Vector2 mousePos;
    private DeadSparkVFX vfx_parent;

    [SerializeField]
    private float aoeRadius;

    [SerializeField]
    private float damage;

    [SerializeField]
    private int targetsHit;
    protected bool firing;

    //public GameObject bulletSpawner;
    //public Transform bulletTransform;

    public override void Activate(GameObject parent)
    {
        Debug.Log("Dead Spark Activated");
        firing = true;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.deadSpark, parent.transform.position);
        SetOnCooldown();

        vfx_parent = parent.transform.GetComponentInChildren<DeadSparkVFX>();

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

            HashSet<Entity> previouslyHit = new HashSet<Entity>();
            
            Vector3 target = mousePos;

            Debug.Log("Firing Spark1!!! (AT : " + target + ")");

            Entity next_enemy = FindNearestEnemy(mousePos, previouslyHit);
            Entity prev_enemy = parent.GetComponent<Player>();;

            if (next_enemy == null) {
                vfx_parent.SendSpark(prev_enemy, target, 0);
                firing = false;
                return;
            }

            Player player = parent.GetComponent<Player>();

            for (int i = 0; i < targetsHit; i++) {
                // TODO: Fix for when enemy dies, we get a nre
                vfx_parent.SendSpark(prev_enemy, next_enemy, i);
                previouslyHit.Add(next_enemy);
                player.DealDamage(next_enemy, damage);
                prev_enemy = next_enemy;
                next_enemy = FindNearestEnemy(prev_enemy.transform.position, previouslyHit);
                if (next_enemy == null)
                    break;
            }


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

    private Enemy FindNearestEnemy(Vector3 center, HashSet<Entity> previouslyHit) {
        // Mask for enemies
        LayerMask mask = LayerMask.GetMask("Enemy");
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(mask);
        List<Collider2D> enemiesHit = new List<Collider2D>();
        int i = Physics2D.OverlapCircle(center, aoeRadius, filter, enemiesHit); 

        Enemy nearestEnemy = null;

        foreach(Collider2D collider in enemiesHit) {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (previouslyHit.Contains(enemy) == true) 
                continue;

            if (nearestEnemy == null || Vector3.Distance(center, enemy.transform.position) < Vector3.Distance(center, nearestEnemy.transform.position))
                nearestEnemy = enemy;
        }
        return nearestEnemy;
    }

}