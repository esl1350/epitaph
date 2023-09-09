using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Controller
{
    private bool isColliding;
    private Enemy enemy;
    
    public bool IsColliding {
        get {return isColliding;}
    }

    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            enemy.DealDamage(other.gameObject.GetComponent<Player>(), enemy.Attack);
            isColliding = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            isColliding = false;
        }
    }
}
