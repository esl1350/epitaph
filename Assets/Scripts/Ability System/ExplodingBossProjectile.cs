using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBossProjectile : BossProjectile
{

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var entity = other.GetComponent<Entity>();
            entity.GetComponent<Entity>().DealDamage(entity, damage);
            // Uncomment this to add in status effects when the projectile hits the player
            //var statusEffectManager = player.GetComponent<StatusEffectManager>();
            //    statusEffectManager?.ApplyEffects(_statusEffects);
            StartCoroutine(ExplodeThenDie());
        }
    }

    private IEnumerator ExplodeThenDie()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        Animator animator = GetComponent<Animator>();
        animator.SetBool("explode", true);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
