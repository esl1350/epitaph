using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class FireCone : MonoBehaviour
{
    [SerializeField]
    private float damagePerTick = 2;
    [SerializeField]
    private float tick = 0.5f;
    [SerializeField]
    private float activeTime = 3f;

    private float lastTick;
    private float startTime;
    private bool doDamage = true;

    public GameObject parent;

    protected void Start()
    {
        startTime = Time.time;
        lastTick = Time.time - (tick/2);
    }
    protected void Update()
    {
        float currentTime = Time.time;
        if (currentTime - startTime < activeTime)
        {
            if (!doDamage && currentTime - lastTick > tick)
            {
                lastTick = currentTime;
                doDamage = true;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (doDamage)
            {
                Entity playerInFire = other.gameObject.GetComponent<Entity>();
                parent.GetComponent<Entity>().DealDamage(playerInFire, damagePerTick);
                doDamage = false;
            }
        }
    }
}
