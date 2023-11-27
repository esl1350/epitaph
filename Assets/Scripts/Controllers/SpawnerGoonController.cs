using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SpawnerGoonController : EnemyController
{

    public GameObject spawningEnemy;

    public Transform spawnPos;

    [SerializeField] public float shortestSpawnTime;

    [SerializeField] public float longestSpawnTime;

    public float spawnRadius = 5f;

    public float spawnAmount = 2f;

    private float timer;

    private float randomTime;

    private Animator sigilAnimator;

    private UnityEngine.Vector2 randomPos;

    protected override void Start(){
        randomTime = Random.Range(shortestSpawnTime, longestSpawnTime + 1);
        sigilAnimator = transform.GetChild(1).gameObject.GetComponent<Animator>();
    }
    protected void Update(){
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.velocity = Vector2.zero;
        GameObject target = GameObject.FindWithTag("Player");
        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            // Spawn Bullet or whatever else
            float i = 0;
            while (i < spawnAmount) {
                Spawn();
                i=i+1;
            }
            timer = 0;
            randomTime = Random.Range(shortestSpawnTime, longestSpawnTime + 1);
            }
        }
    void Spawn(){
        sigilAnimator.SetTrigger("is summoning");
        //logic to spawn
        Vector2 randomPos = (Random.insideUnitCircle * spawnRadius);
        randomPos += (Vector2)spawnPos.position;
        Instantiate(spawningEnemy, randomPos, Quaternion.identity, this.transform.parent);
    }    
}
