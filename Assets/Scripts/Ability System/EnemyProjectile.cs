using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    public GameObject target;
    private Enemy enemy;
    private EntityStats stats;
    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = target.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        Vector3 rotation = transform.position;
        float rot = Mathf.Atan2(-1.0f * direction.y, -1.0f * direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rot - 90f, Vector3.forward);
        enemy = parent.GetComponent<Enemy>();

        stats = enemy.EntityStats;
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            var entity = other.GetComponent<Entity>();
            entity.GetComponent<Entity>().DealDamage(entity, damage);
            // Uncomment this to add in status effects when the projectile hits the player
            //var statusEffectManager = player.GetComponent<StatusEffectManager>();
            //    statusEffectManager?.ApplyEffects(_statusEffects);
        }
        Destroy(this.gameObject);

    }
}
