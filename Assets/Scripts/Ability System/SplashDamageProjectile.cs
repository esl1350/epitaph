using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamageProjectile : PlayerProjectile
{
    [SerializeField] private float radius;
    [SerializeField] private float splashDamage;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Hit Splash {other.gameObject.name}");
        _animator.SetTrigger("Collide");  
        rb.velocity = new Vector2(direction.x, direction.y).normalized * 0;
        if (other.gameObject.tag == "Enemy") {
            base.OnTriggerEnter2D(other);
        }
        Collider2D[] hit = Physics2D.OverlapCircleAll(gameObject.transform.position, radius, LayerMask.GetMask("Enemy"));
        foreach (Collider2D collider in hit) {
            Enemy enemy = collider.GetComponent<Enemy>();
            var statusEffectManager = enemy.GetComponent<StatusEffectManager>();
                statusEffectManager?.ApplyEffects(_statusEffects);
            var entity = other.GetComponent<Entity>();
            parent.GetComponent<Entity>().DealDamage(enemy, splashDamage);
        }
        StartCoroutine(CheckAnimationAndDestroy());
        //Destroy(this.gameObject);
    }
}
