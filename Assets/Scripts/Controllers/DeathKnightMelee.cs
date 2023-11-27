using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathKnightMelee : MonoBehaviour
{
    private Animator animator;
    private Vector2 offset;
    private BoxCollider2D meleeHitbox;
    private Entity source;
    public float knockbackDuration;
    public float knockbackForce;
    // Start is called before the first frame update
    void Start()
    {
        source = transform.parent.gameObject.GetComponent<Entity>();
        meleeHitbox = GetComponent<BoxCollider2D>();
        meleeHitbox.enabled = false;
        Vector2 parentCol = transform.parent.gameObject.GetComponent<BoxCollider2D>().size;
        offset = new Vector2(parentCol.x/2 + meleeHitbox.size.x/2, parentCol.y + meleeHitbox.size.y/3);
    }

    public void Attack(Vector3 vecToPlayer) {
        if (vecToPlayer.y > 0){
            transform.localPosition = new Vector2(0, offset.y);
        } else {
            transform.localPosition = new Vector2(0, -1.0f*offset.y);
        }
        StartCoroutine(ActivateHitbox());
    }

    public IEnumerator ActivateHitbox() {
        SetActive();
        yield return new WaitForSeconds(0.3f);
        SetInactive();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player player = other.GetComponent<Player>();
            source.DealDamage(player, source.EntityStats.GetStatValue(StatEnum.ATTACK));

            var kb = other.GetComponent<Knockback>();
            kb?.KnockbackCustomForce(source.gameObject, knockbackForce, knockbackDuration);
        }
     }

    public void SetActive() {
        meleeHitbox.enabled = true;
    }

    public void SetInactive() {
        meleeHitbox.enabled = false;
    }
}
