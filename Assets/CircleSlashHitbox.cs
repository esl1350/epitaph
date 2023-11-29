using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSlashHitbox : MonoBehaviour
{
    public GameObject parent;
    public float damage;
    [SerializeField] protected List<StatusEffect> _statusEffects;
    public float knockbackForce;
    public float knockbackDuration;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            GameObject.FindWithTag("CMCam").GetComponent<CameraShake>().Shake(1.5f, 0.3f);
            Enemy enemy = other.GetComponent<Enemy>();
            parent.GetComponent<Entity>().DealDamage(enemy, damage);
            var statusEffectManager = enemy.GetComponent<StatusEffectManager>();
                statusEffectManager?.ApplyEffects(_statusEffects);
            var kb = other.GetComponent<Knockback>();
            kb?.KnockbackCustomForce(parent.GetComponent<Entity>().gameObject, knockbackForce, knockbackDuration);
        }
        
        // Debug.Log($"Hit {other.gameObject.name}");
    }
}
