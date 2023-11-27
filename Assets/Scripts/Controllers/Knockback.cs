using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackMultiplier; // how much force is applied on knockback
    [SerializeField] private float knockbackDurationMultiplier; // how long "CC" is

    private Rigidbody2D body;
    private Controller controller;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
    }

    public void KnockbackCustomForce(GameObject applier, float force, float duration) {
        controller.IsKnockedBack = true;
        StopAllCoroutines();
        body.velocity = Vector3.zero;
        Vector2 direction = (transform.position - applier.transform.position).normalized;
        body.AddForce(direction * force * knockbackMultiplier, ForceMode2D.Impulse);
        StartCoroutine(ResetKnockBack(duration));
    }
    private IEnumerator ResetKnockBack(float timer) {
        controller.CanMove = false;
        yield return new WaitForSeconds(timer * knockbackDurationMultiplier);
        body.velocity = Vector3.zero;
        controller.CanMove = true;
        controller.IsKnockedBack = false;

    }
}
