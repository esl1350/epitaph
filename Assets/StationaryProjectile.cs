using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryProjectile : PlayerProjectile
{

    [SerializeField]
    private float aoeRadius = 3;

    // Start is called before the first frame update
    protected override void Start()
    {
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
       
    }
    
    protected override void Update()
    {
        if (projectileTimer > 0)
        {
            projectileTimer -= Time.deltaTime;
            return;
        }

        Destroy(this.gameObject);
    }

}
