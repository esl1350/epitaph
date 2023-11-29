using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public enum AttackDirection {
        left, right, up, down
    }
    private Animator animator;
    public AttackDirection attackDir;
    public float meleeKnockback;
    public float meleeKnockbackDuration;
    private Vector2 offset;
    private BoxCollider2D meleeHitbox;
    private Entity source;
    private bool rotated;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.gameObject.GetComponent<Animator>();
        source = transform.parent.gameObject.GetComponent<Entity>();
        attackDir = AttackDirection.right;
        rotated = false;
        meleeHitbox = GetComponent<BoxCollider2D>();
        meleeHitbox.enabled = false;


        Vector2 parentCol = transform.parent.gameObject.GetComponent<CapsuleCollider2D>().size;
        
        offset = new Vector2(parentCol.x/2 + meleeHitbox.size.x/2, parentCol.y/2 + meleeHitbox.size.y/2);
    }

    public void Attack(Vector3 mousePos) {
        DetermineAttackDir(mousePos);
        rotated = false;
        //Debug.Log(attackDir);
        switch(attackDir) {
            case AttackDirection.left:
                transform.localPosition = new Vector2(-1.0f * offset.x, 0);
                animator.SetInteger("attack dir", 3);
                break;
            case AttackDirection.right:
                transform.localPosition = new Vector2(offset.x, 0);
                animator.SetInteger("attack dir", 1);
                break;
            case AttackDirection.up:
                rotated = true;
                transform.Rotate(new Vector3(0, 0, 90));
                transform.localPosition = new Vector2(0, offset.y);
                animator.SetInteger("attack dir", 2);

                break;
            case AttackDirection.down:
                rotated = true;
                transform.Rotate(new Vector3(0, 0, 90));
                animator.SetInteger("attack dir", 0);
                transform.localPosition = new Vector2(0, -1.0f*offset.y);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            Enemy enemy = other.GetComponent<Enemy>();
            source.DealDamage(enemy, source.EntityStats.GetStatValue(StatEnum.ATTACK));

            var kb = other.GetComponent<Knockback>();
            kb?.KnockbackCustomForce(source.gameObject, meleeKnockback, meleeKnockbackDuration);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy Projectile")) {
            Destroy(other.gameObject);
        }
     }

    public void SetActive() {
        meleeHitbox.enabled = true;
    }

    public void SetInactive() {
        meleeHitbox.enabled = false;
        if (rotated) {
            transform.Rotate(new Vector3(0, 0, 90));
        }
    }

    private void DetermineAttackDir(Vector3 relativeMousePos) {
        Vector2 absPos = new Vector2(Mathf.Abs(relativeMousePos.x), Mathf.Abs(relativeMousePos.y));
        if (absPos.x >= absPos.y) {
            if (relativeMousePos.x > 0) {
                attackDir = AttackDirection.right;
                return;
            } else {
                attackDir = AttackDirection.left;
                return;
            }
        } else {
            if (relativeMousePos.y > 0) {
                attackDir = AttackDirection.up;
                return;
            }
        }
        attackDir = AttackDirection.down;
        return;
    }
}
