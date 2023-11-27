using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Aura Augment", menuName = "Augments/Damage Aura")]
public class DamageAuraAugment : Augment {
    [SerializeField]
    public float radius;
    [SerializeField]    
    public float damage;
    [SerializeField]   
    public float interval;

    [SerializeField]
    private Sprite vfxSprite;

    public override IEnumerator _Enable(GameObject parent, GameObject vfxObject) {
        // Do stuff with vfxObject
        if (vfxObject.TryGetComponent(out SpriteRenderer spriteRenderer)) {
            spriteRenderer.sprite = vfxSprite;
            spriteRenderer.enabled = true;
        }

        // Return Coroutine
        if (parent.TryGetComponent(out Entity player)) {
            return PassiveDamageCoroutine(player);
        }
        return null;
    }

    private IEnumerator PassiveDamageCoroutine(Entity player)
    {
        while (true)
        {
            LayerMask mask = LayerMask.GetMask("Enemy");

            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(mask);
            List<Collider2D> enemiesHit = new List<Collider2D>();
            int i = Physics2D.OverlapCircle(player.transform.position, radius, filter, enemiesHit);

            foreach (Collider2D collider in enemiesHit)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                player.DealDamage(enemy, damage);
            }
            yield return new WaitForSeconds(interval);
        }
        
    }
    public override void _Disable(GameObject parent, GameObject vfxObject) {
        // Do stuff with vfx object
        if (vfxObject.TryGetComponent(out SpriteRenderer spriteRenderer)) {
            spriteRenderer.enabled = false;
        }
    }

}