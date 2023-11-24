using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathKnightController : EnemyController
{
    private GameObject target;

    private float timer;

    public float distanceAway;

    public float unloadSpeed;
    private DeathKnightMelee melee;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
        timer = 0f;
        melee = transform.GetChild(0).GetComponent<DeathKnightMelee>();
    }

    protected override void Update() {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (timer < unloadSpeed) {
            timer += Time.deltaTime;
        }
        if (dist < distanceAway)
        {
            
            agent.velocity = Vector2.zero;
            if (timer > unloadSpeed)
            {
                StartCoroutine(AttackSequence(target.transform.position - transform.position));
                timer = 0f;
            }
        }
        // animator.SetFloat("vel x", agent.velocity.x);
        // animator.SetFloat("vel y", agent.velocity.y);
        // if (agent.velocity.magnitude < 0.05) {
        //     animator.SetBool("is stopped", false);
        // } else {
        //     animator.SetBool("is stopped", false);
        // }
    }

    public IEnumerator AttackSequence(Vector3 dir) {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.32f, 0.28f);
        yield return new WaitForSeconds(0.12f);
        GetComponent<SpriteRenderer>().color = Color.white;
        StartCoroutine(AttackDelay(dir));


    }

    public IEnumerator AttackDelay(Vector3 dir) {
        yield return new WaitForSeconds(0.2f);
        melee.Attack(dir);
    }

}

