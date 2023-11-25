using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedGoonController : EnemyController
{
    private GameObject target;
    public EnemyProjectile projectile;
    private float timer;

    public float distanceAway;

    public float unloadSpeed;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }

    protected override void Update() {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < distanceAway && dist > fleeDist)
        {
            agent.velocity = Vector2.zero; 
        }
        animator.SetFloat("vel x", agent.velocity.x);
        animator.SetFloat("vel y", agent.velocity.y);
        if (agent.velocity.magnitude < 0.05) {
            animator.SetBool("is stopped", false);
        } else {
            animator.SetBool("is stopped", false);
        }
    }

    void Shoot(){
        animator.SetTrigger("is casting");

        Projectile proj = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        proj.parent = gameObject;

    }
}
