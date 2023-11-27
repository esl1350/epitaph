using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGoon : Enemy
{
    private Animator sigilAnimator;
    protected override void Start() {
        base.Start();
        sigilAnimator = transform.GetChild(1).gameObject.GetComponent<Animator>();
    }
    public override void Die() {
        animator.SetFloat("health percent", 0f);
        sigilAnimator.SetBool("is dead", true);
    }

    void Update() {
        animator.SetFloat("health percent", 100f * Health.GetStatValue() / intialHealth);
    }

    public void DestroyAfterDeathAnim() {
        base.Die();
    }
}
