using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoonController : EnemyController
{
    [SerializeField] private float damageInterval;
    private float timer;
    private GameObject player;
    private UnityEngine.AI.NavMeshAgent agent;

    protected override void Start() {
        base.Start();
        timer = 0f;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    protected override void Update() {
        animator.SetFloat("vel x", agent.velocity.x);
        animator.SetFloat("vel y", agent.velocity.y);
        if (agent.velocity.magnitude < 0.05) {
            animator.SetBool("is stopped", true);
        } else {
            animator.SetBool("is stopped", false);
        }
        if (this.isColliding) {
            timer += Time.deltaTime;
            if (timer > damageInterval) {
                enemy.DealDamage(player.GetComponent<Player>(), stats.GetStatValue(StatEnum.ATTACK));
                timer = 0f;
            }
        } else {
            timer = 0f;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other) {
        base.OnCollisionEnter2D(other);
        if (other.gameObject.tag == "Player") {
            player = other.gameObject;
            enemy.DealDamage(player.GetComponent<Player>(), stats.GetStatValue(StatEnum.ATTACK));
        }
    }
}
