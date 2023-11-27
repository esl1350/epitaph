using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.EventSystems.EventTrigger;

public class PushBackIndicator : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 3f;
    [SerializeField]
    private float damage = 5f;

    public float meleeKnockback;
    public float meleeKnockbackDuration;

    private float startTime;

    public GameObject parent;

    protected void Start()
    {
        startTime = Time.time;
    }
    protected void Update()
    {
        float currentTime = Time.time;
        if (currentTime - startTime > activeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("knocking back");
            Entity player = other.gameObject.GetComponent<Entity>();
            parent.GetComponent<Entity>().DealDamage(player, damage);

            var kb = other.GetComponent<Knockback>();
            kb?.KnockbackCustomForce(parent.gameObject, meleeKnockback, meleeKnockbackDuration);
        }
    }
}
