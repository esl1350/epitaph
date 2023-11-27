// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class LightGrenadeProjectile : SplashDamageProjectile
// {
//     [SerializeField] private float radius;
//     [SerializeField] private float splashDamage;

//     protected override void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.gameObject.tag == "Enemy") {
//             _animator.SetTrigger("Collide");  
//             StartCoroutine(CheckAnimationAndDestroy());
//             base.OnTriggerEnter2D(other);
//         }
//         //Debug.Log($"Hit {other.gameObject.name}");
//         // if (other.gameObject.tag == "Enemy") {
//         //     _animator.SetTrigger("Collide");
//         //     rb.velocity = new Vector2(direction.x, direction.y).normalized * 0;

//         //     // var statusEffectManager = other.GetComponent<StatusEffectManager>();
//         //     //     statusEffectManager?.ApplyEffects(_statusEffects);
//         //     var entity = other.GetComponent<Entity>();
//         //     // if (entity != null) {
//         //     //     parent.GetComponent<Entity>().DealDamage(entity, damage);
//         //     // }
            
//         //     Collider2D[] hit = Physics2D.OverlapCircleAll(gameObject.transform.position, radius, LayerMask.GetMask("Enemy"));
//         //     Debug.Log(hit);
//         //     foreach (Collider2D collider in hit) {
//         //         Enemy enemy = collider.GetComponent<Enemy>();
//         //         parent.GetComponent<Entity>().DealDamage(enemy, splashDamage);
//         //         var statusEffectManager = enemy.GetComponent<StatusEffectManager>();
//         //             //statusEffectManager?.ApplyEffects(_statusEffects);
//         //         if (statusEffectManager != null) {
//         //             Debug.Log($"Applying status effects to: {enemy.gameObject.name}");
//         //             statusEffectManager.ApplyEffects(_statusEffects);
//         //         } else {
//         //             Debug.Log($"No StatusEffectManager found on: {enemy.gameObject.name}");
//         //         }
//         //     }

//         //     StartCoroutine(CheckAnimationAndDestroy());
//         // }

//         //Destroy(this.gameObject);
//     }


//     private IEnumerator CheckAnimationAndDestroy()
//     {
//         // Assuming the animation is in the first layer, index 0
//         while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
//         {
//             // Wait for the next frame
//             yield return null;
//         }

//         // Destroy the GameObject after the animation is complete
//         Destroy(gameObject);
//     }
// }
