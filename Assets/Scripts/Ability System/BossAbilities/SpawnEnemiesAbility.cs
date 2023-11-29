using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesAbility : BossAbility
{
    public GameObject indicator;
    public List<GameObject> enemyPrefabs;

    private List<GameObject> enemySpawners;

    [SerializeField]
    private float spawningTime;

    public override void AbilityBehavior(GameObject parent)
    {
        enemySpawners = new List<GameObject>();
        StartCoroutine(SpawnForSomeTime(parent));
    }
    IEnumerator SpawnForSomeTime(GameObject parent)
    {
        enemySpawners = new List<GameObject>();
        // Make the enemy spawners then I guess they do their things for a bit
        Crystal[] crystals = parent.transform.parent.transform.GetComponentsInChildren<Crystal>();

        foreach (Crystal crystal in crystals)
        {
            int random = Random.Range(0, enemyPrefabs.Count);
            if ( random ==  enemyPrefabs.Count - 1)
            {
                random = Random.Range(2, enemyPrefabs.Count);
            }
            GameObject spawner = Instantiate(enemyPrefabs[random], crystal.transform.position, Quaternion.identity);
            enemySpawners.Add(spawner);
        }

        yield return new WaitForSeconds(spawningTime);

        for (int i = enemySpawners.Count - 1; i >= 0; i--)
        {
            Destroy(enemySpawners[i]);
        }

        Destroy(this.gameObject);
    }
}
