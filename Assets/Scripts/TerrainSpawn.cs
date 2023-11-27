using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawn : MonoBehaviour
{

    //really basic terrain spawn for proof of concept for even more basic game functions lol

    [SerializeField] private GameObject terrain;
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject player;
    private Vector2 bounds;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = player.transform.position;
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = new Vector2(this.transform.position.x, player.transform.position.y);
        this.transform.position = newPos;
    }

    private void Spawn() {
        Vector2 spawnPos = this.transform.position;
        spawnPos.x += Random.Range(bounds.x*-1, bounds.x);
        spawnPos.y += 3.0f * bounds.y / 2.0f;
        GameObject newTerrain = Instantiate(terrain, spawnPos, Quaternion.identity);
    }
}
