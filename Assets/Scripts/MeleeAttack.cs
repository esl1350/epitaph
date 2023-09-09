using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public enum AttackDirection {
        left, right, up, down
    }
    public AttackDirection attackDir;
    private Vector2 offset;
    private BoxCollider2D meleeHitbox;
    private SpriteRenderer hitboxRenderer;
    private Entity source;
    private bool rotated;
    // Start is called before the first frame update
    void Start()
    {
        source = transform.parent.gameObject.GetComponent<Entity>();
        attackDir = AttackDirection.right;
        rotated = false;
        meleeHitbox = GetComponent<BoxCollider2D>();
        meleeHitbox.enabled = false;
        hitboxRenderer = GetComponent<SpriteRenderer>();
        hitboxRenderer.enabled = false;

        Vector2 parentCol = transform.parent.gameObject.GetComponent<BoxCollider2D>().size;
        
        offset = new Vector2(parentCol.x/2 + meleeHitbox.size.x/2, parentCol.y/2 + meleeHitbox.size.y/2);
    }

    public void Attack(Vector3 mousePos) {
        DetermineAttackDir(mousePos);
        rotated = false;
        switch(attackDir) {
            case AttackDirection.left:
                transform.localPosition = new Vector2(-1.0f * offset.x, 0);
                break;
            case AttackDirection.right:
                transform.localPosition = new Vector2(offset.x, 0);
                break;
            case AttackDirection.up:
                rotated = true;
                transform.Rotate(new Vector3(0, 0, 90));
                transform.localPosition = new Vector2(0, offset.y);
                break;
            case AttackDirection.down:
                rotated = true;
                transform.Rotate(new Vector3(0, 0, 90));
                transform.localPosition = new Vector2(0, -1.0f*offset.y);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            Enemy enemy = other.GetComponent<Enemy>();
            source.DealDamage(enemy , source.Attack);

            //knockback here?
        }
     }

    public void SetActive() {
        meleeHitbox.enabled = true;
        hitboxRenderer.enabled = true;
    }

    public void SetInactive() {
        meleeHitbox.enabled = false;
        hitboxRenderer.enabled = false;
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
